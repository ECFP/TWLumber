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

## Sources

- [Legna Software — TallyWorks](https://www.legnasoftware.com/tallyworks)
- [Legna Software](https://www.legnasoftware.com/)
