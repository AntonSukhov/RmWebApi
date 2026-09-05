# RM WebApi

ASP.NET Core Web API application (.NET 9) for managing **work types** (WorkType), **work units** (WorkUnit) and **contract performers** (Performer).

---

## Table of Contents

1. [General Architecture](#general-architecture)
2. [Layers and Project Purpose](#layers-and-project-purpose)
3. [Project Dependency Diagram](#project-dependency-diagram)
4. [UML Class Diagram of the Performer Domain](#uml-class-diagram-of-the-performer-domain)
5. [Database Model (ER Diagram)](#database-model-er-diagram)
6. [Database Selection](#database-selection)
7. [Build and Run](#build-and-run)
8. [Testing](#testing)

---

## General Architecture

The project follows a layered **Clean Architecture** style. Each layer only knows about the layer *below* it through its **abstractions** (interfaces). Concrete implementations are wired only at the composition point — the root project `RM.WebApi` (Composition Root).

```mermaid
flowchart TB
    Client["👤 Client<br/>(HTTP / Swagger)"]

    subgraph Presentation ["Presentation Layer"]
        WebApi["RM.WebApi<br/>(Controllers, Middleware, DI)"]
        Api["RM.Api<br/>(DTO + typed HttpClient)"]
    end

    subgraph Business ["Business Logic Layer"]
        BLL["RM.BLL<br/>(Services, Validators)"]
        BLLAbstractions["RM.BLL.Abstractions<br/>(service interfaces, models)"]
    end

    subgraph Data ["Data Access Layer"]
        DAL["RM.DAL<br/>(base repositories, DbContext)"]
        DALMsSql["RM.DAL.MsSql<br/>(EF Core: SqlServer)"]
        DALPostgreSql["RM.DAL.PostgreSql<br/>(EF Core: Npgsql)"]
        DALAbstractions["RM.DAL.Abstractions<br/>(entities, repository interfaces)"]
    end

    Common["RM.Common<br/>(constants, helpers)"]

    Client -->|HTTP| WebApi
    WebApi -->|uses| Api
    WebApi -->|calls via interfaces| BLLAbstractions
    BLL -.->|implements| BLLAbstractions
    BLL -->|uses via interfaces| DALAbstractions
    DAL -.->|implements| DALAbstractions
    DALMsSql -.->|inherits| DAL
    DALPostgreSql -.->|inherits| DAL
    WebApi -->|"Composition Root:<br/>chooses implementation"| DALMsSql
    WebApi -->|"Composition Root:<br/>chooses implementation"| DALPostgreSql
    Common --- WebApi
    Common --- BLL
    Common --- DAL
```

**Key principles:**

| Principle | How it is implemented |
|---|---|
| Dependency Inversion | BLL and DAL depend on abstractions, not implementations |
| Composition Root | All «interface → implementation» bindings live in `RM.WebApi/Extensions/` (`ServiceCollectionExtensions.cs`) |
| Contract isolation | Each layer has its own DTOs: `Entity` → `Model` → `Response` |
| Swappable database | MS SQL / PostgreSQL is selected via the `DataStorageType` configuration |

---

## Layers and Project Purpose

| Project | Layer | Purpose |
|---|---|---|
| `RM.WebApi` | Presentation | Controllers, Startup, DI registrations, Swagger, error-handling middleware (BLL exceptions → HTTP status codes: 400/404/409/500) |
| `RM.Api` | Presentation (client) | Request/Response DTOs, generated NSwag client |
| `RM.BLL` | Business Logic | Services (`WorkType`, `WorkUnit`, `Performer`), FluentValidation validators, AutoMapper profiles |
| `RM.BLL.Abstractions` | Business Logic | Service interfaces, models (`PerformerModel`, `PageOptionsModel`), validator interfaces |
| `RM.DAL` | Data Access | Base `ContractGpdDbContextBase`, base repository implementations |
| `RM.DAL.MsSql` | Data Access | Concrete EF Core DbContext for MS SQL Server |
| `RM.DAL.PostgreSql` | Data Access | Concrete EF Core DbContext for PostgreSQL |
| `RM.DAL.Abstractions` | Data Access | Entities (`WorkTypeEntity`, `WorkUnitEntity`, `PerformerEntity`), repository interfaces |
| `RM.Common` | Shared | Constants (`DatabaseConstants`, `ApiConstants`), helpers |
| `RM.BLL.Tests` | Tests | xUnit tests for BLL with mocks (Moq) |

---

## Project Dependency Diagram

Arrow direction means «depends on». All dependencies point strictly «downward», there are no cycles.

```mermaid
flowchart TD
    WebApi["RM.WebApi"]

    subgraph Abstractions ["Abstractions (contracts)"]
        BLLA["RM.BLL.Abstractions"]
        DALA["RM.DAL.Abstractions"]
    end

    subgraph Implementations ["Implementations"]
        BLL["RM.BLL"]
        DAL["RM.DAL"]
        DALMs["RM.DAL.MsSql"]
        DALPg["RM.DAL.PostgreSql"]
    end

    Api["RM.Api"]
    Common["RM.Common"]
    Tests["RM.BLL.Tests"]

    WebApi --> Api
    WebApi --> BLLA
    WebApi --> BLL
    WebApi --> DALA
    WebApi --> DAL
    WebApi --> DALMs
    WebApi --> DALPg
    WebApi --> Common

    BLL --> BLLA
    BLL --> DALA
    DAL --> DALA
    DALMs --> DAL
    DALPg --> DAL
    Tests --> BLLA
    Tests --> BLL
    Tests --> DALA

    style WebApi fill:#4472c4,color:#fff
    style Abstractions fill:#70ad47,color:#fff
    style Implementations fill:#ed7d31,color:#fff
```

---

## UML Class Diagram of the Performer Domain

End-to-end data path through the layers: `PerformerEntity` → `PerformerModel` → `PerformerResponse`. Each class lives in its own layer and does not depend on its neighbors directly — AutoMapper connects them.

```mermaid
classDiagram
    direction TB

    class IPerformerRepository {
        <<interface>>
        <<RM.DAL.Abstractions>>
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
        +DeleteAsync(performerId) int
    }
    class PerformerRepository {
        <<RM.DAL>>
        -dbContext : ContractGpdDbContextBase
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
        +DeleteAsync(performerId) int
    }
    class PerformerEntity {
        <<RM.DAL.Abstractions>>
        +Guid Id
        +Guid EntityId
        +DateTime CreateDate
        +DateTime? EditDate
        +string Creator
        +string? Editor
        +string Snils
        +string? Inn
        +string Surname
        +string Name
        +string Patronymic
        +GenderEnum Gender
        +... passport data (8 fields)
    }

    class IPerformerService {
        <<interface>>
        <<RM.BLL.Abstractions>>
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
        +DeleteAsync(performerId)
    }
    class PerformerService {
        <<RM.BLL>>
        -performerRepository : IPerformerRepository
        -pageOptionsValidator : IPageOptionsValidator
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
        +DeleteAsync(performerId)
    }
    class PerformerModel {
        <<RM.BLL.Abstractions>>
        +Guid Id
        +GenderEnum Gender
        +string Surname
        +string Name
        +string Patronymic
        +... passport data
    }

    class PerformerApiController {
        <<RM.WebApi>>
        -performerService : IPerformerService
        -performerApiMappers : IPerformerApiMappers
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
        +DeleteAsync(performerId)
    }
    class PerformerResponse {
        <<RM.Api>>
        +Guid Id
        +GenderEnum Gender
        +string Surname
        +string Name
        +string Patronymic
        +... passport data
    }

    IPerformerRepository <|.. PerformerRepository : implements
    IPerformerService <|.. PerformerService : implements
    PerformerRepository --> PerformerEntity : returns
    PerformerService --> IPerformerRepository : depends on
    PerformerService --> PerformerModel : returns
    PerformerApiController --> IPerformerService : depends on
    PerformerApiController --> PerformerResponse : returns

    style IPerformerRepository fill:#70ad47,color:#fff
    style IPerformerService fill:#70ad47,color:#fff
    style PerformerEntity fill:#ed7d31
    style PerformerModel fill:#ed7d31
    style PerformerResponse fill:#ed7d31
```

---

## Database Model (ER Diagram)

The `DbContracts` database (PostgreSQL / MS SQL):

```mermaid
erDiagram
    WORK_UNITS ||--o{ WORK_TYPES : "FK_WorkTypes_WorkUnits"
    PERFORMERS {
        uuid Id PK
        uuid EntityId
        varchar Creator
        varchar Editor
        varchar Snils
        varchar Inn
        varchar Surname
        varchar Name
        varchar Patronymic
        int Gender
        varchar PassportNumber
        varchar PassportSeries
        varchar PassportIssuePlace
        datetime PassportIssueDate
        varchar PassportDepartmentCode
        varchar PassportBirthPlace
        datetime PassportBirthDate
        varchar PassportRegistrationPlace
    }
    WORK_UNITS {
        smallint Id PK
        varchar Name "max 50"
    }
    WORK_TYPES {
        uuid Id PK
        varchar Name "max 200"
        smallint WorkUnitId FK "nullable"
    }
```

**Relationships:**

- `WorkUnits` (1) ──< `WorkTypes` (M): one work unit is used by many work types; `WorkTypes.WorkUnitId` can be `NULL`.
- `Performers` — a standalone entity (contract performers).

---

## Database Selection

The storage backend is selected once at application startup from configuration:

```mermaid
flowchart LR
    Start["Startup.RegisterDbContexts"] --> Read{"DataStorageType<br/>from configuration"}
    Read -->|MSSQL| MsSql["ContractGpdDbContext<br/>(UseSqlServer)"]
    Read -->|PostgreSQL| Pg["ContractGpdDbContext<br/>(UseNpgsql)"]
    Read -->|other| Err["InvalidOperationException"]
    MsSql --> DI["AddDbContext<br/>(base → concrete context)"]
    Pg --> DI
```

The `DataStorageType` value (`"MSSQL"` / `"PostgreSQL"`) and connection strings are set in `appsettings*.json` / User Secrets.

---

## Build and Run

```bash
# Build
cd RM
dotnet build RM.sln

# Run (Development, ports 5000/5001)
cd RM.WebApi
dotnet run

# Swagger (in Development)
# https://localhost:5001/swagger
```

### Configuration

| Setting | Description |
|---|---|
| `DataStorageType` | Storage type: `"MSSQL"` or `"PostgreSQL"` |
| `ConnectionStrings:MsSqlDbContractConnection` | MS SQL connection string |
| `ConnectionStrings:PostgreDbContractConnection` | PostgreSQL connection string |

---

## Testing

```bash
cd RM.BLL.Tests
dotnet test
```

BLL tests are isolated from the database: repositories are mocked with Moq, while AutoMapper profiles are used for real.