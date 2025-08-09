# ASEE-Personal-Finance-Management-Project

## Personal Finance Management API 

This project is a .NET 8 web API for managing financial transactions and categories, storing data in PostgreSQL. It follows the Clean Architecture principles with clear separation of concerns between Domain, Application, Infrastructure, and API layers.
It supports importing CSV files, categorizing and splitting transactions, and retrieving paginated and filtered transaction lists.
It also provides automatic categorization of transactionsm and generating analytics reports.

## Key Features:

- Import Categories from CSV
- Import Transactions from CSV
- Transaction Categorization — manually assign categories to transactions
- Transaction Splitting — split transaction into 2 or more splits
- Automatic Categorization — assign categories based on MCC, description, or rules
- List Transactions with filtering, sorting, and pagination
- Analytics — get transaction sums and counts per category (and optionally per subcategory)

## Architecture Notes: 

- CQRS: Commands for imports, categorization, and splits; Queries for retrieval
- Repository + Unit of Work: Database abstraction
- EntityTypeConfiguration: Explicit EF Core mappings
- OperationResult: Consistent API responses
- Clean Architecture: clear separation between business logic, persistence, and presentation layers

## Technologies
 - .NET 8 / C# - Backend
 - PostgreSQL with Entity Framework Core - Database
 - Angular v20 - Frontend

## API Endpoints
### Categories
- POST /categories/import
- GET /categories
### Transactions
- POST /transactions/import
- GET /transactions
- POST /transactions/{id}/categorize
- POST /transactions/{id}/split
- POST /transactions/auto-categorize
### Spending-analytics
- GET /spending-analytics
