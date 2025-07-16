# Finance Transaction CRUD API

This is a production-grade ASP.NET Core Web API for managing financial transactions, using Entity Framework Core and SQL Server.

## Supported .NET Versions
- .NET 6.0
- .NET 7.0

## Framework & Pattern
- Uses **ASP.NET Core MVC** for RESTful CRUD operations
- Repository and service layers (OOP best practices)
- SQL Server database (EF Core)
- Swagger UI for API testing

## Getting Started

### 1. Prerequisites
- .NET 6 SDK or later
- SQL Server instance

### 2. Configuration
- Update `appsettings.json` with your SQL Server connection string:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=FinanceDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
  ```

### 3. Database Migration
- Run EF Core migrations to create the database (if using EF Core migrations):
  ```sh
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```

### 4. Run the API
- Build and run the project:
  ```sh
  dotnet run
  ```
- The API will be available at `http://localhost:5000` (or as configured).
- Swagger UI: `http://localhost:5000/swagger`

## API Endpoints
- `GET /api/Transaction` - List all transactions
- `GET /api/Transaction/{id}` - Get transaction by ID
- `POST /api/Transaction` - Create a new transaction
- `PUT /api/Transaction/{id}` - Update a transaction
- `DELETE /api/Transaction/{id}` - Delete a transaction

## Project Structure
- `Transaction.cs` - Entity model
- `FinanceDbContext.cs` - EF Core DbContext
- `ITransactionRepository.cs` / `TransactionRepository.cs` - Repository pattern
- `ITransactionService.cs` / `TransactionService.cs` - Service layer
- `TransactionController.cs` - API controller
- `Startup.cs` / `Program.cs` - App configuration

## Testing & Coverage

### Run Tests
- Navigate to the `CRUD` folder
- Run:
  ```sh
  dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
  ```
- This will run all unit and integration tests and generate a coverage report (in `lcov` format)

### Coverage Goal
- The provided tests target 90%+ code coverage for repository, service, and controller layers
- Review the coverage report for details

---

**Note:** Replace `YOUR_SERVER` in the connection string with your SQL Server instance name. 