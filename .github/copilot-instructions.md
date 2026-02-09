# Copilot Instructions for ValgfagPortfolio

## Build, Test, and Development Commands

### Building and Running
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application (localhost:5000)
dotnet run
```

### Database Migrations
```bash
# Create a new migration (replace "MigrationName" with your migration name)
dotnet ef migrations add MigrationName

# Apply pending migrations to database
dotnet ef database update

# View current migration status
dotnet ef migrations list
```

### Important Setup Notes
- **appsettings.json is gitignored** - You must create your own local copy
- **Database**: Uses MSSQL (SQL Server). The project includes a local SQLite fallback (app.db) for quick testing
- **Admin seeding**: An `AdminSeed` class in `/Data/AdminSeed.cs` is called from `Program.cs` to seed an initial admin user. Remember to change the default email and password

## Architecture Overview

This is an ASP.NET Core 10 Blazor Server application for a portfolio site that manages elective subjects (categories), posts, and references.

### Three-Layer Pattern
The codebase follows a clean three-layer architecture:

1. **Data Layer** (`/Data/`)
   - `ApplicationDbContext.cs`: EF Core DbContext for MSSQL
   - `ApplicationUser.cs`: ASP.NET Core Identity user class
   - `AdminSeed.cs`: Initial admin user seeding logic

2. **Repository/Persistence Layer** (`/Persistence/`)
   - `/Repositories/`: Generic implementations (CategoryRepository, PostRepository, ReferenceRepository)
   - `/Interfaces/IRepository<T>`: Generic CRUD interface used by all repositories
   - Each repository wraps Entity Framework operations with Include() for eager loading of related entities

3. **Service/Business Logic Layer** (`/Services/`)
   - `/Services/CategoryService`, `/Services/PostService`, `/Services/ReferenceService`: Business logic and validation
   - `/Services/Interfaces/ICategoryService`, etc.: Service contracts
   - `BlobStorageService.cs`: Handles Azure Blob Storage for file uploads
   - `MarkdownFileService.cs`: Markdown processing utilities

### Data Model
Key entities in `/Model/`:
- **Category**: Hierarchical (has ParentCategory and Children navigation properties)
- **Post**: Content items linked to categories
- **Reference**: External references/links
- **AppUser**: Application user representation

### UI Components
Blazor components in `/Components/` use MudBlazor (Material Design UI library):
- `/Components/Pages/`: Page components
- `/Components/Category/`: Category-related components
- `/Components/Posts/`: Post-related components
- `/Components/TopCategories/`: Featured categories display
- `/Components/Layout/`: Navigation and layout wrappers
- `/Components/Account/`: Authentication pages

## Key Conventions

### Repository Pattern
- All data access goes through `IRepository<T>` implementations
- Repository methods always use `.Include()` for related entities (e.g., CategoryRepository includes ParentCategory and Children)
- Always use async/await patterns (method names end with `Async`)

### Service Layer
- Services receive repositories through dependency injection
- Services validate input before passing to repositories
- Services perform business logic (e.g., checking for null/empty before creating)

### Dependency Injection
- Registered in `Program.cs` using `builder.Services.AddScoped<>`
- Pattern: `AddScoped<IRepository<T>, ConcreteRepository>()` for repositories
- Pattern: `AddScoped<IService, ConcreteService>()` for services

### Entity Framework
- Uses `#nullable enable` and implicit using statements
- Target framework: .NET 10.0
- Database provider: SqlServer (with SqliteDbContext available as fallback)

### Blazor Components
- Uses MudBlazor for UI components (v8.15.0)
- Components are in `/Components/` with `.razor` extension
- Supports interactive server-side rendering (`AddInteractiveServerComponents`)

### Azure Integration
- **User Secrets**: Configured for sensitive data (connection strings, storage credentials)
- Use `builder.Configuration.AddUserSecrets<Program>()` for local development secrets
- Azure Blob Storage for file uploads via BlobStorageService

### Database
- Default: SQL Server
- Local development: SQLite fallback (app.db)
- Migrations tracked in `/Migrations/` folder
