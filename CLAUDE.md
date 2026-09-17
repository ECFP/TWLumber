# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

TWLumber is a .NET 10 client library for reading data from a **Legna TallyWorks** lumber ERP database (Microsoft SQL Server). The goal is strongly-typed models and repositories over the TallyWorks schema (sales orders, purchase orders, production runs, inventory, invoicing, shipments, customers, vendors) so consumers don't write raw SQL. It is an independent project, not produced by Legna Software.

**Design constraint:** no ORM dependency. Data access is plain ADO.NET via `Microsoft.Data.SqlClient` against the TallyWorks tables directly.

## Commands

The solution uses the new XML solution format (`TWLumber.slnx`).

```bash
dotnet build TWLumber.slnx
dotnet run --project src/TWLumber.Client.Sample
dotnet pack src/TWLumber.Client -c Release -o artifacts
```

Package metadata (ID, version, license, repo URL, SourceLink/symbols) lives in `src/TWLumber.Client/TWLumber.Client.csproj`; bump `<Version>` there for releases. The root `README.md` is packed as the NuGet readme.

There is no test project yet.

## Architecture

`src/TWLumber.Client` is the library; `src/TWLumber.Client.Sample` is a console app for exercising it against a real database.

- **`TWClient`** is the single entry point. Constructed from `TWClientOptions` (or a bare connection string), it validates options, normalizes the connection string (injecting `ApplicationName` if absent), and lazily exposes repositories as properties: `Sales`, `SaleItems`, `Transfers` (public) and `SaleItemDetails` (internal — details reach callers only via `SaleItem.SaleItemDetails`).
- **Connections are per-operation.** Internal `CreateConnection()` / `OpenConnectionAsync()` hand back a new `SqlConnection` the caller disposes. Each repository has its own private `CreateCommand` that applies `CommandTimeoutSeconds` and binds parameters; new repositories should follow that shape.
- **`Data/*Repository.cs`** hold the SQL (`SELECT * FROM <Table> WHERE ...`, always parameterized). String lookups are trimmed in C# and compared with `UPPER()` on both sides, so callers can pass any casing. `Data/*Mapper.cs` build a column-name map from the reader once per result set, then map rows; missing columns and `NULL`s become the type's default instead of throwing, so partial `SELECT`s are safe.
- **Hierarchy loading.** `Sale` → `SaleItem` → `SaleItemDetail`. `includeItems: true` on a `SalesRepository` query attaches items via one batched `WHERE SalesOrder IN (...)` query (and one more for details), not one query per sale. Batches are capped at `SaleItemsBatchLimitException.DefaultLimit` (2000, under SQL Server's ~2100 parameter ceiling); exceeding it throws unless `TWClientOptions.AllowLargeIncludeQueries` is set, which splits the work into sequential batches. Details are matched to parent items by a case-insensitive `(SalesOrder, SaleItemNumber)` composite key.
- **Models** (`Models/`) mirror TallyWorks tables 1:1 with XML docs naming each source column and SQL type: `Sale`/`SALES`, `SaleItem`/`SALEITEMS`, `SaleItemDetail`/`SALEITEMDETAIL`, `Transfer`/`TRANSFERS`. Some properties are friendly aliases over the raw column (e.g. `SaleItem.Pieces` → `QuantityP`, `BF` → `QuantityI`). `Enums/` holds `SalesStatus` (mapped to `Sale.Status`) and `SalesShipmentStatus` (documented on `Sale.ShipmentStatus`, which is still a `string`).

## Conventions

From `.editorconfig`: file-scoped namespaces (warning), `readonly` fields where possible (warning), 4-space indent, CRLF line endings, UTF-8 with BOM, final newline. Projects enable nullable reference types and implicit usings. Public APIs carry XML doc comments; async methods take a `CancellationToken` and use `ConfigureAwait(false)`.
