# TWLumber

A lightweight .NET client for managing data from a **Legna Tallyworks** lumber database 
(Microsoft SQL Server). It exposes strongly-typed models and repositories over the Tallyworks 
schema so applications can handle Tallyworks objects without writing raw SQL or taking on an ORM 
dependency.

## About Legna Tallyworks

[Legna Software](https://www.legnasoftware.com/) is a Winston-Salem, NC company that has built
software for the wood-products industry for 30+ years. Its flagship product, **TallyWorks**, is an
ERP/production-management suite for sawmills, hardwood mills, remanufacturing and millwork plants,
and distribution centers. TallyWorks Lumber manages operations from primary breakdown through sale
and shipment — providing real-time inventory and production visibility, production costing, mobile
yard/tally operations, and reporting (including Power BI integration). It powers daily operations at
200+ mills across the US and Canada.

Tallyworks stores its data in a Microsoft SQL Server database (the schema this library targets uses
a database named like `TWLumber`). Core entities include sales orders, purchase orders, production
runs, inventory, invoicing, shipments, customers and vendors. This client reads the SQL tables 
directly so the programmer doesn't have to write that data layer.

> This is an independent client for the Tallyworks database. It is not produced or endorsed by 
Legna Software. We would always appreciate any help Legna would like to provide to this repo.

## Requirements

- **.NET 10** (`net10.0`)
- Network access to a Tallyworks SQL Server instance and a connection string with read permissions
- [`Microsoft.Data.SqlClient`](https://www.nuget.org/packages/Microsoft.Data.SqlClient) (referenced
  by the library)

## Getting started

Add the package to your application:

```bash
dotnet add package TWLumber.Client
```

(Or reference `src/TWLumber.Client/TWLumber.Client.csproj` directly when working from a clone.)
Then create a `TWClient` from a connection string:

```csharp
using TWLumber.Client;
using TWLumber.Client.Enums;
using TWLumber.Client.Models;

var client = new TWClient(
    "Data Source=your-sql-host;Database=TWLumber;Integrated Security=True;TrustServerCertificate=True;");

// Verify connectivity.
if (await client.TestConnectionAsync())
{
    Console.WriteLine("Connected to the Tallyworks database.");
}
```

For more control, construct it from `TWClientOptions`:

```csharp
var client = new TWClient(new TWClientOptions
{
    ConnectionString = "Data Source=your-sql-host;Database=TWLumber;Integrated Security=True;TrustServerCertificate=True;",
    CommandTimeoutSeconds = 30,          // default
    ApplicationName = "MyApp",           // reported to SQL Server for connection tracking
    AllowLargeIncludeQueries = false,    // see "Loading related data" below
});
```

## Usage

The client exposes repositories as properties. Every query method is asynchronous and accepts an
optional `CancellationToken`.

### Sales

```csharp
// A single sale by sales order number (optionally with its line items).
Sale? sale = await client.Sales.GetBySalesOrderAsync("123456", includeItems: true);
if (sale is not null)
{
    Console.WriteLine($"{sale.SalesOrder}: {sale.CustCode}, status {sale.Status}, {sale.SaleItems.Count} item(s)");
    foreach (SaleItem item in sale.SaleItems)
    {
        Console.WriteLine($"  {item.SaleItemNumber}: {item.Pieces} pieces / {item.BF} BF");
    }
}

// Multiple sales, by various keys (all support the includeItems flag).
IReadOnlyList<Sale> byCustomer    = await client.Sales.GetByCustomerAsync("CustomerX");
IReadOnlyList<Sale> closed        = await client.Sales.GetByStatusAsync(SalesStatus.Closed);
IReadOnlyList<Sale> bySalesPerson = await client.Sales.GetBySalesPersonAsync("XX");
IReadOnlyList<Sale> byLocation    = await client.Sales.GetByLocationAsync("Dallas");
IReadOnlyList<Sale> older         = await client.Sales.GetEnteredBeforeAsync(new DateTime(2026, 9, 15));
```

String lookups are trimmed and matched **case-insensitively**, so casing and surrounding whitespace
don't matter.

### Sale items

Line items are normally reached through a `Sale` (via `includeItems: true`), but you can also query
them directly for a given order. Each `SaleItem` carries its `SaleItemDetails`.

```csharp
IReadOnlyList<SaleItem> items = await client.SaleItems.GetBySalesOrderAsync("123456");
```

### Status labels

`SalesStatus` (and `SalesShipmentStatus`, which documents the values behind the raw
`Sale.ShipmentStatus` string) carry the labels Tallyworks displays. `ToDisplayName()` returns that
label, falling back to the member name:

```csharp
SalesStatus.ClosedAuto.ToDisplayName(); // "Closed - Automatically"
SalesStatus.Open.ToDisplayName();       // "Open"
```

### Transfers

```csharp
Transfer? transfer = await client.Transfers.GetByTransferIdAsync(123456);
IReadOnlyList<Transfer> forRun = await client.Transfers.GetByRunAsync("RUN NAME HERE");
```

### Loading related data

`Sale` → `SaleItem` → `SaleItemDetail` form a hierarchy. When you request `includeItems: true`, the
client loads all items for the matched sales in a single batched query (and their details in
another) rather than issuing one query per sale, then wires them onto the parent objects.

To guard against accidentally broad queries, a single batch is capped at **2,000 distinct sales
orders** (kept under SQL Server's parameter ceiling). If an include would exceed that, the call
throws `SaleItemsBatchLimitException` by default. If you genuinely need to load items for a very
large set of sales, set `TWClientOptions.AllowLargeIncludeQueries = true` to split the work into
sequential batches instead:

```csharp
try
{
    var manySales = await client.Sales.GetByStatusAsync(SalesStatus.Closed, includeItems: true);
}
catch (SaleItemsBatchLimitException ex)
{
    // Narrow the query, or enable AllowLargeIncludeQueries — but note every matching
    // item row is held in memory, and latency grows with the result size.
    Console.WriteLine($"{ex.RequestedCount} orders exceeds the batch limit of {ex.Limit}.");
}
```

## Models

Models are plain classes that mirror the Tallyworks tables. Each property is documented with its
source column name and SQL type. Mapping from the database is tolerant: columns absent from a result
set (for example, from a partial `SELECT`) or `NULL` values become the type's default rather than
throwing.

| Model            | Table            | Notes                                                    |
|------------------|------------------|----------------------------------------------------------|
| `Sale`           | `SALES`          | A sales order; has many `SaleItems`.                     |
| `SaleItem`       | `SALEITEMS`      | A line item; keyed by (`SalesOrder`, `SaleItemNumber`); has many `SaleItemDetails`. |
| `SaleItemDetail` | `SALEITEMDETAIL` | Per-item detail rows; loaded with their parent item.     |
| `Transfer`       | `TRANSFERS`      | A transfer linked to a production run.                   |

## Building

This is a .NET 10 solution (`TWLumber.slnx`) with two projects:

- `src/TWLumber.Client` — the client library.
- `src/TWLumber.Client.Sample` — the console application used to test the library.

```bash
dotnet build TWLumber.slnx
```

The library is packaged as the `TWLumber.Client` NuGet package; package metadata lives in
`src/TWLumber.Client/TWLumber.Client.csproj`.

```bash
dotnet pack src/TWLumber.Client -c Release -o artifacts
```

## Sources

- [Legna Software — TallyWorks](https://www.legnasoftware.com/tallyworks)
- [Legna Software](https://www.legnasoftware.com/)
