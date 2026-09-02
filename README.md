# RM WebApi

ASP.NET Core Web API-приложение (.NET 9) для управления **видами работ** (WorkType), **единицами работ** (WorkUnit) и **исполнителями договоров** (Performer).

> Подробный контекст проекта (для AI-агентов) — см. [KODA.md](KODA.md).

---

## Содержание

1. [Общая архитектура](#общая-архитектура)
2. [Слои и назначение проектов](#слои-и-назначение-проектов)
3. [Диаграмма зависимостей проектов](#диаграмма-зависимостей-проектов)
4. [Поток запроса (Sequence Diagram)](#поток-запроса-sequence-diagram)
5. [UML-диаграмма классов домена Performer](#uml-диаграмма-классов-домена-performer)
6. [Модель данных БД (ER-диаграмма)](#модель-данных-бд-er-диаграмма)
7. [Выбор СУБД](#выбор-субд)
8. [Сборка и запуск](#сборка-и-запуск)
9. [Тестирование](#тестирование)

---

## Общая архитектура

Проект построен по слоистой архитектуре в стиле **Clean Architecture**. Каждый слой знает только о слое *ниже* через его **абстракции** (интерфейсы). Конкретные реализации подключаются только в точке сборки — корневом проекте `RM.WebApi` (Composition Root).

```mermaid
flowchart TB
    Client["👤 Клиент<br/>(HTTP / Swagger)"]

    subgraph Presentation ["Слой представления"]
        WebApi["RM.WebApi<br/>(Controllers, Middleware, DI)"]
        Api["RM.Api<br/>(DTO + typed HttpClient)"]
    end

    subgraph Business ["Слой бизнес-логики"]
        BLL["RM.BLL<br/>(Services, Validators)"]
        BLLAbstractions["RM.BLL.Abstractions<br/>(интерфейсы сервисов, модели)"]
    end

    subgraph Data ["Слой доступа к данным"]
        DAL["RM.DAL<br/>(базовые репозитории, DbContext)"]
        DALMsSql["RM.DAL.MsSql<br/>(EF Core: SqlServer)"]
        DALPostgreSql["RM.DAL.PostgreSql<br/>(EF Core: Npgsql)"]
        DALAbstractions["RM.DAL.Abstractions<br/>(сущности, интерфейсы репозиториев)"]
    end

    Common["RM.Common<br/>(константы, утилиты)"]

    Client -->|HTTP| WebApi
    WebApi -->|использует| Api
    WebApi -->|вызывает через интерфейсы| BLLAbstractions
    BLL -.->|реализует| BLLAbstractions
    BLL -->|использует через интерфейсы| DALAbstractions
    DAL -.->|реализует| DALAbstractions
    DALMsSql -.->|наследует| DAL
    DALPostgreSql -.->|наследует| DAL
    WebApi -->|"Composition Root:<br/>выбирает реализацию"| DALMsSql
    WebApi -->|"Composition Root:<br/>выбирает реализацию"| DALPostgreSql
    Common --- WebApi
    Common --- BLL
    Common --- DAL
```

**Ключевые принципы:**

| Принцип | Как реализован |
|---|---|
| Инверсия зависимостей | BLL и DAL зависят от абстракций, а не реализаций |
| Composition Root | Все привязки «интерфейс → реализация» — в `RM.WebApi/Extensions/` (`ServiceCollectionExtensions.cs`) |
| Изоляция контрактов | У каждого слоя свои DTO: `Entity` → `Model` → `Response` |
| Сменяемость СУБД | Выбор MS SQL / PostgreSQL по конфигурации `DataStorageType` |

---

## Слои и назначение проектов

| Проект | Слой | Назначение |
|---|---|---|
| `RM.WebApi` | Presentation | Контроллеры, Startup, DI-регистрации, Swagger, мидлвар ошибок |
| `RM.Api` | Presentation (клиент) | Request/Response DTO, сгенерированный NSwag-клиент |
| `RM.BLL` | Business Logic | Сервисы (`WorkType`, `WorkUnit`, `Performer`), FluentValidation-валидаторы, AutoMapper-профили |
| `RM.BLL.Abstractions` | Business Logic | Интерфейсы сервисов, модели (`PerformerModel`, `PageOptionsModel`), интерфейсы валидаторов |
| `RM.DAL` | Data Access | Базовый `ContractGpdDbContextBase`, базовые реализации репозиториев |
| `RM.DAL.MsSql` | Data Access | Конкретный EF Core DbContext для MS SQL Server |
| `RM.DAL.PostgreSql` | Data Access | Конкретный EF Core DbContext для PostgreSQL |
| `RM.DAL.Abstractions` | Data Access | Сущности (`WorkTypeEntity`, `WorkUnitEntity`, `PerformerEntity`), интерфейсы репозиториев |
| `RM.Common` | Общее | Константы (`DatabaseConstants`, `ApiConstants`), хелперы |
| `RM.BLL.Tests` | Тесты | xUnit-тесты BLL с моками (Moq) |

---

## Диаграмма зависимостей проектов

Направление стрелок — «зависит от». Все зависимости идут строго «вниз», циклов нет.

```mermaid
flowchart TD
    WebApi["RM.WebApi"]

    subgraph Abstractions ["Абстракции (контракты)"]
        BLLA["RM.BLL.Abstractions"]
        DALA["RM.DAL.Abstractions"]
    end

    subgraph Implementations ["Реализации"]
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

## Поток запроса (Sequence Diagram)

Пример: `GET /api/performer/all?pageNumber=1&pageSize=100`

```mermaid
sequenceDiagram
    autonumber
    actor C as Клиент
    participant MW as ErrorHandlingMiddleware
    participant Ctrl as PerformerApiController<br/>(RM.WebApi)
    participant Svc as PerformerService<br/>(RM.BLL)
    participant Val as PageOptionsValidator<br/>(RM.BLL)
    participant Repo as PerformerRepository<br/>(RM.DAL)
    participant DB as PostgreSQL / MS SQL

    C->>MW: GET /api/performer/all
    MW->>Ctrl: вызов действия GetAllAsync
    Ctrl->>Ctrl: PageOptionsRequest → PageOptionsModel<br/>(IPageOptionsApiMappers)
    Ctrl->>Svc: GetAllAsync(pageOptions)
    Svc->>Val: ValidateAndThrowAsync(pageOptions)
    Val-->>Svc: OK (или ValidationException → 400)
    Svc->>Svc: PageOptionsModel → Shared.PageOptionsModel<br/>(IPageOptionsBllMappers)
    Svc->>Repo: GetAllAsync(pageOptions)
    Repo->>DB: SELECT ... LIMIT/OFFSET (AsNoTracking)
    DB-->>Repo: IReadOnlyCollection&lt;PerformerEntity&gt;
    Repo-->>Svc: entities
    Svc->>Svc: PerformerEntity → PerformerModel<br/>(IPerformerBllMappers)
    Svc-->>Ctrl: IReadOnlyCollection&lt;PerformerModel&gt;
    Ctrl->>Ctrl: PerformerModel → PerformerResponse<br/>(IPerformerApiMappers)
    Ctrl-->>MW: IEnumerable&lt;PerformerResponse&gt;
    MW-->>C: 200 OK (JSON)
```

Обработка ошибок: исключения BLL (`ConflictException`, `DataNotFoundException`, `ValidationException`) перехватываются `ErrorHandlingMiddleware` и преобразуются в соответствующие HTTP-коды (400/404/409/500).

---

## UML-диаграмма классов домена Performer

Сквозной путь данных через слои: `PerformerEntity` → `PerformerModel` → `PerformerResponse`. Каждый класс живёт в своём слое и не зависит от соседних напрямую — связывает их AutoMapper.

```mermaid
classDiagram
    direction TB

    class IPerformerRepository {
        <<interface>>
        <<RM.DAL.Abstractions>>
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
    }
    class PerformerRepository {
        <<RM.DAL>>
        -dbContext : ContractGpdDbContextBase
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
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
        +... паспортные данные (8 полей)
    }

    class IPerformerService {
        <<interface>>
        <<RM.BLL.Abstractions>>
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
    }
    class PerformerService {
        <<RM.BLL>>
        -performerRepository : IPerformerRepository
        -pageOptionsValidator : IPageOptionsValidator
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
    }
    class PerformerModel {
        <<RM.BLL.Abstractions>>
        +Guid Id
        +GenderEnum Gender
        +string Surname
        +string Name
        +string Patronymic
        +... паспортные данные
    }

    class PerformerApiController {
        <<RM.WebApi>>
        -performerService : IPerformerService
        -performerApiMappers : IPerformerApiMappers
        +GetAllAsync(pageOptions)
        +GetByIdAsync(performerId)
    }
    class PerformerResponse {
        <<RM.Api>>
        +Guid Id
        +GenderEnum Gender
        +string Surname
        +string Name
        +string Patronymic
        +... паспортные данные
    }

    IPerformerRepository <|.. PerformerRepository : implements
    IPerformerService <|.. PerformerService : implements
    PerformerRepository --> PerformerEntity : возвращает
    PerformerService --> IPerformerRepository : зависит от
    PerformerService --> PerformerModel : возвращает
    PerformerApiController --> IPerformerService : зависит от
    PerformerApiController --> PerformerResponse : возвращает

    style IPerformerRepository fill:#70ad47,color:#fff
    style IPerformerService fill:#70ad47,color:#fff
    style PerformerEntity fill:#ed7d31
    style PerformerModel fill:#ed7d31
    style PerformerResponse fill:#ed7d31
```

---

## Модель данных БД (ER-диаграмма)

База данных `DbContracts` (PostgreSQL / MS SQL):

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

**Связи:**

- `WorkUnits` (1) ──< `WorkTypes` (M): одна единица работы используется во многих видах работ; `WorkTypes.WorkUnitId` может быть `NULL`.
- `Performers` — самостоятельная сущность (исполнители договоров).

---

## Выбор СУБД

Хранилище выбирается один раз при старте приложения по конфигурации:

```mermaid
flowchart LR
    Start["Startup.RegisterDbContexts"] --> Read{"DataStorageType<br/>из конфигурации"}
    Read -->|MSSQL| MsSql["ContractGpdDbContext<br/>(UseSqlServer)"]
    Read -->|PostgreSQL| Pg["ContractGpdDbContext<br/>(UseNpgsql)"]
    Read -->|другое| Err["InvalidOperationException"]
    MsSql --> DI["AddDbContext<br/>(базовый → конкретный контекст)"]
    Pg --> DI
```

Значение `DataStorageType` (`"MSSQL"` / `"PostgreSQL"`) и строки подключения задаются в `appsettings*.json` / User Secrets.

---

## Сборка и запуск

```bash
# Сборка
cd RM
dotnet build RM.sln

# Запуск (Development, порты 5000/5001)
cd RM.WebApi
dotnet run

# Swagger (в Development)
# https://localhost:5001/swagger
```

### Конфигурация

| Параметр | Описание |
|---|---|
| `DataStorageType` | Тип хранилища: `"MSSQL"` или `"PostgreSQL"` |
| `ConnectionStrings:MsSqlDbContractConnection` | Строка подключения к MS SQL |
| `ConnectionStrings:PostgreDbContractConnection` | Строка подключения к PostgreSQL |

---

## Тестирование

```bash
cd RM.BLL.Tests
dotnet test
```

Тесты BLL изолированы от БД: репозитории мокаются через Moq, AutoMapper-профили подключаются реально.