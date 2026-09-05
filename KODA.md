# KODA.md — Контекст проекта RM

## Обзор проекта

**RM** — это ASP.NET Core Web API-приложение (.NET 9) для управления **видами работ** (WorkType), **единицами работ** (WorkUnit) и **исполнителями договоров** (Performer). Приложение реализует CRUD-операции с сущностями и предоставляет REST API с пагинацией и валидацией.

Проект построен по слоистой архитектуре (Clean Architecture) с чётким разделением на уровни:
- **API-слой** (WebApi + Api)
- **BLL (Business Logic Layer)** — бизнес-логика
- **DAL (Data Access Layer)** — доступ к данным через репозитории и EF Core
- **Abstractions** — интерфейсы и общие сущности
- **Common** — общие утилиты и константы

Проект поддерживает **два бэкенд-хранилища**: MS SQL Server и PostgreSQL. Выбор СУБД определяется конфигурацией (`DataStorageType`).

**Стадия развития:** проект находится на начальном этапе. Архитектура закладывалась с расчётом на расширение — в будущем будут добавляться новые контроллеры, сущности и домены. Реализованы базовые функционалы для WorkType, WorkUnit и Performer.

---

## Инфраструктура и Docker-окружение

Проект запускается на машине под управлением **Ubuntu**. Базы данных и вспомогательные сервисы работают в Docker-контейнерах.

### Docker Сеть
Все контейнеры подключены к пользовательской сети **`subd-net`**. Это позволяет им обращаться друг к другу по именам контейнеров, а не по `localhost`.

### Контейнеры
1.  **PostgreSQL**
    *   Имя контейнера: `postgresql-container`
    *   Порт: `5432`
    *   База данных: `postgres`
    *   Пользователь/пароль: хранятся в User Secrets (не публикуются)
2.  **MS SQL Server**
    *   Имя контейнера: `mssql-container`
    *   Порт: `1433`
3.  **MCP-сервер (Postgres MCP Pro)**
    *   Имя контейнера: `mcp-postgres`
    *   Порт: `8000` (SSE)
    *   Назначение: Обеспечивает доступ AI-агентов (Koda/Cursor) к базе данных.
    *   Подключение к БД: через сеть `subd-net` (хост `postgresql-container`).

### Взаимодействие
Для подключения к БД из других сервисов (включая MCP-сервер) используйте имя хоста `postgresql-container`.

---

## Структура проекта

```
RM/
├── RM.sln                          # Решение (10 проектов)
├── RM.WebApi/                      # Главный веб-проект (ASP.NET Core 9)
│   ├── Controllers/                # REST-контроллеры (WorkType, WorkUnit, Performer)
│   ├── Extensions/                 # Регистрация сервисов (DI)
│   ├── Filters/                    # Document-фильтры Swagger (EnumNamesDocumentFilter)
│   ├── Mapping/                    # AutoMapper-профили и кастомные мапперы API
│   ├── Middleware/                 # Кастомный мидлвар (ErrorHandlingMiddleware)
│   ├── Pages/                      # Razor Pages (базовая страница)
│   ├── Properties/                 # launchSettings.json
│   ├── wwwroot/                    # Статические файлы (Swagger UI)
│   ├── Startup.cs                  # Конфигурация приложения (Startup-паттерн)
│   ├── Program.cs                  # Точка входа (minimal hosting)
│   └── appsettings*.json           # Конфигурация (Dev/Prod)
├── RM.Api/                         # Клиентская библиотека для потребления API
│   ├── DTOs/                       # Request/Response DTO (подпапки Requests/Responses)
│   ├── Enums/                      # Enum-модели клиента (GenderEnum)
│   ├── GeneratedApiClients/        # Сгенерированный NSwag клиент (в .gitignore)
│   ├── Mapping/Extensions/         # Мапперы DTO
│   ├── Services/                   # API-сервисы (typed HttpClient)
│   ├── Scripts/                    # generate-client.sh
│   ├── nswag.json                  # Конфигурация NSwag
│   └── OpenApiSpecs/               # OpenAPI spec (RmWebApi.v1.json)
├── RM.BLL/                         # Бизнес-логика
│   ├── Services/                   # Реализации сервисов (WorkType, WorkUnit, Performer)
│   ├── Validators/                 # FluentValidation валидаторы
│   ├── Mapping/                    # AutoMapper-профили BLL
│   ├── Exceptions/                 # Кастомные исключения (Conflict, DataNotFound)
│   └── Extensions/                 # Расширения BLL
├── RM.BLL.Abstractions/            # Интерфейсы BLL
│   ├── Services/                   # IWorkTypeService, IWorkUnitService, IPerformerService
│   ├── Models/                     # DTO модели (creation/updation/pagination)
│   ├── Enums/                      # Enum-модели (GenderEnum)
│   ├── Validators/                 # Интерфейсы валидаторов
│   └── Errors/                     # Абстракции ошибок
├── RM.DAL/                         # Data Access Layer (общий)
│   ├── DbContexts/                 # ContractGpdDbContextBase
│   ├── Repositories/               # Реализации репозиториев
│   ├── Mapping/                    # AutoMapper-профили DAL
├── RM.DAL.Abstractions/            # Интерфейсы DAL
│   ├── Entities/                   # WorkTypeEntity, WorkUnitEntity, WorkTypeShortEntity, PerformerEntity
│   └── Repositories/               # IWorkTypeRepository, IWorkUnitRepository, IPerformerRepository
├── RM.DAL.MsSql/                   # Реализация EF Core для MS SQL Server
│   └── DbContexts/                 # ContractGpdDbContext (SqlServer)
├── RM.DAL.PostgreSql/              # Реализация EF Core для PostgreSQL
│   └── DbContexts/                 # ContractGpdDbContext (Npgsql)
├── RM.Common/                      # Общие утилиты
│   ├── Constants/                  # DatabaseConstants, ApiConstants, HttpConstants
│   └── Helpers/                    # Утилиты (ConfigurationHelper)
└── RM.BLL.Tests/                   # xUnit тесты (BLL)
    ├── Services/                   # Тесты сервисов (подпапки WorkTypeService, WorkUnitService, PerformerService)
    └── TestSupport/                # Моки и поддержка
```

---

## Технологии и зависимости

### Фреймворк
- **.NET 9** (net9.0)
- **ASP.NET Core** (Web API + Razor Pages)

### ORM / Данные
- **Entity Framework Core 9.0.16**
- **Microsoft.EntityFrameworkCore.SqlServer 9.0.16** (MS SQL)
- **Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4** (PostgreSQL)

### Маппинг
- **AutoMapper 16.1.1**
- Собственные интерфейсы мапперов (`IMapper<TFrom, TTo>`)

### Валидация
- **FluentValidation.AspNetCore 11.3.1**

### Документация API
- **Swashbuckle.AspNetCore 10.1.7** (+ Swashbuckle.AspNetCore.Annotations)

### Тестирование
- **xUnit 2.9.3**
- **Moq 4.20.72**
- **xunit.runner.visualstudio 3.1.5**
- **coverlet.collector 10.0.1**
- **Microsoft.NET.Test.Sdk 18.5.1**

### Клиент API
- **NSwag** — генерация typed HTTP-клиента из OpenAPI spec

### Внутренние пакеты-обёртки (Infrastructure.*)
- `Infrastructure.AspNetCore 1.0.1` — базовый Startup, Swagger
- `Infrastructure.Mapping.AutoMapper 1.0.0` — маппер `IMapper<TFrom, TTo>`
- `Infrastructure.Disposable 1.0.1` — `DisposableBase`
- `Infrastructure.EntityFramework 1.0.4` — `DbContextBase`
- `Infrastructure.Shared 1.0.6` — общие утилиты
- `Infrastructure.Testing 1.0.5` / `Infrastructure.Testing.XUnit 1.0.2` — тестовые утилиты
- `IdentityWebApp.Api 3.2.1` — интеграция с Identity

### Настройки проекта
- `<Nullable>enable</Nullable>` — во всех проектах
- `<UserSecretsId>` — для dev-конфигурации (секреты)
- `<GenerateDocumentationFile>true</GenerateDocumentationFile>` — XML-документация

---

## Сборка и запуск

### Сборка
```bash
cd RM
dotnet build RM.sln
```

### Запуск
```bash
cd RM/RM.WebApi
dotnet run
```

### Запуск с профилем Development (с user secrets)
```bash
cd RM/RM.WebApi
dotnet run --environment Development
```

### Тесты
```bash
cd RM/RM.BLL.Tests
dotnet test
```

### Публикация
```bash
cd RM/RM.WebApi
dotnet publish -c Release -o publish
```

---

## Конфигурация

### Переменные окружения
- **DataStorageType** — тип хранилища: `"MSSQL"` или `"PostgreSQL"` (по умолчанию из `appsettings.json`: `"PostgreSQL"`)
- **ConnectionStrings:MsSqlDbContractConnection** — строка подключения к MS SQL
- **ConnectionStrings:PostgreDbContractConnection** — строка подключения к PostgreSQL

### Файлы конфигурации
- `appsettings.json` — базовая конфигурация
- `appsettings.Development.json` — настройки для dev
- `appsettings.Production.json` — настройки для prod
- **User Secrets** — для локальной разработки (строки подключения и чувствительные данные)

---

## Архитектура и зависимости между проектами

```
RM.WebApi
  ├── RM.Api              (клиентская библиотека)
  ├── RM.BLL.Abstractions (интерфейсы)
  ├── RM.BLL              (бизнес-логика)
  ├── RM.DAL.Abstractions (сущности + интерфейсы репозиториев)
  ├── RM.DAL              (базовые репозитории)
  ├── RM.DAL.MsSql        (контекст для MS SQL)
  ├── RM.DAL.PostgreSql   (контекст для PostgreSQL)
  └── RM.Common           (константы)

Зависимости идут "вверх":
  WebApi → Api, BLL, DAL, DAL.MsSql, DAL.PostgreSql
  BLL → BLL.Abstractions, DAL.Abstractions
  DAL → DAL.Abstractions
  DAL.MsSql → DAL
  DAL.PostgreSql → DAL
```

### Слои

1. **WebApi** — точка входа. Контроллеры, Startup, DI-регистрация, Swagger, мидлвары.
2. **Api** — клиентская библиотека с typed HttpClient для потребления этого же API внешними сервисами.
3. **BLL** — бизнес-логика. Сервисы (WorkTypeService, WorkUnitService, PerformerService), валидаторы (FluentValidation), мапперы.
4. **DAL.Abstractions** — сущности (WorkTypeEntity, WorkUnitEntity, PerformerEntity) и интерфейсы репозиториев.
5. **DAL** — базовые DbContext и реализации репозиториев.
6. **DAL.MsSql / DAL.PostgreSql** — конкретные EF Core DbContext'ы для нужной СУБД.
7. **Common** — константы (DatabaseConstants, ApiConstants).

---

## Управление клиентами API (RM.Api)

Для генерации typed HTTP-клиента из OpenAPI spec:

```bash
cd RM/RM.Api/Scripts
./generate-client.sh
```

Скрипт:
1. Восстанавливает .NET tools
2. Генерирует клиент из `OpenApiSpecs/RmWebApi.v1.json` через NSwag
3. Результат записывается в `GeneratedApiClients/RmWebApiClient.cs`

### Имена значений enum в сгенерированном клиенте (x-enumNames)

По умолчанию Swashbuckle 10 сериализует enum в OpenAPI-спецификации только числами
(`"enum": [0, 1]`), из-за чего NSwag генерирует члены enum с именами `_0`, `_1`.
Настройки в `nswag.json` для этого нет — имена значений передаются через
расширение `x-enumNames` в самой спецификации.

Решение — document-фильтр `RM.WebApi/Filters/EnumNamesDocumentFilter.cs`,
зарегистрированный в `AddSwaggerDocumentation()` (ServiceCollectionExtensions):

```csharp
options.DocumentFilter<EnumNamesDocumentFilter>();
```

Фильтр после генерации спецификации находит схемы с enum-значениями в
`components.schemas`, сопоставляет их с CLR-перечислениями **по имени схемы**
и добавляет `x-enumNames` (например, `["Male", "Female"]`).

**Важно (Swashbuckle 10 / Microsoft.OpenApi 2.x):** обычные schema-фильтры
(`ISchemaFilter`) для enum-типов **не вызываются** — поэтому используется именно
document-фильтр.

**Полный процесс после добавления нового enum (2 шага, выполняются вручную):**
1. Запустить API и сохранить `/swagger/v1/swagger.json` → `RM.Api/OpenApiSpecs/RmWebApi.v1.json`
2. Запустить `RM.Api/Scripts/generate-client.sh`

**Что работает автоматически:** nullable enum, enum в коллекциях, `[Flags]`.

**Ограничения:**
- Сопоставление идёт по имени типа: enum-типы должны иметь **уникальные имена**
  в рамках решения (принято соглашение — суффикс `Enum`, например `GenderEnum`).
  При двух enum с одинаковым именем фильтр возьмёт первый попавшийся.
- Не сработает для nested enum (вложенного в класс) и при кастомном
  `SchemaIdSelector`, переименовывающем схемы.

---

## Стиль кодирования и практики

- **C# с включённым Nullable context** — все проекты используют `<Nullable>enable</Nullable>`
- **ImplicitUsings** — включены в нескольких проектах
- **Constructor injection** — все зависимости передаются через конструктор с `ArgumentNullException.ThrowIfNull`
- **Async/await** — все сервисные методы асинхронные
- **Disposal pattern** — сервисы наследуются от `DisposableBase`, реализуют `IDisposable` и `IAsyncDisposable`
- **Валидация** — FluentValidation валидаторы с методом `ValidateAndThrowAsync`
- **Конфликты и исключения** — кастомные исключения (`ConflictException`, `DataNotFoundException`)
- **Swagger Annotations** — используются атрибуты для документирования API
- **Слоистая DI-регистрация** — все сервисы регистрируются в `ServiceCollectionExtensions`

---

## Ключевые домены

### WorkType (Вид работы)
- `Id` (Guid) — идентификатор
- `Name` (string) — название
- `WorkUnitId` (short?, FK) — ссылка на единицу работы
- `WorkUnit` (Navigation) — навигация на единицу работы

### WorkUnit (Единица работы)
- Используется для привязки единиц измерения к видам работ

### Performer (Исполнитель договора)
- `Id` (Guid) — идентификатор
- `EntityId` (Guid) — идентификатор истории
- `CreateDate` (DateTime) — дата создания
- `EditDate` (DateTime?) — дата редактирования
- `Creator` (string) — создатель
- `Editor` (string?) — редактор
- `Snils` (string) — СНИЛС
- `Inn` (string?) — ИНН
- `Surname` (string) — фамилия
- `Name` (string) — имя
- `Patronymic` (string) — отчество
- `Gender` (int) — пол
- `PassportNumber` (string) — номер паспорта
- `PassportSeries` (string) — серия паспорта
- `PassportIssuePlace` (string) — место выдачи паспорта
- `PassportIssueDate` (DateTime) — дата выдачи паспорта
- `PassportDepartmentCode` (string) — код подразделения
- `PassportBirthPlace` (string) — место рождения
- `PassportBirthDate` (DateTime) — дата рождения
- `PassportRegistrationPlace` (string) — место регистрации

### CRUD операции
#### WorkType (`api/work-type`)
- `GET /api/work-type/all` — получить список (с пагинацией)
- `GET /api/work-type/{workTypeId:guid}` — получить по ID
- `POST /api/work-type` — создать
- `PUT /api/work-type` — обновить
- `DELETE /api/work-type/{workTypeId:guid}` — удалить

#### WorkUnit (`api/work-unit`)
- `GET /api/work-unit` — получить список

#### Performer (`api/performer`)
- `GET /api/performer/all` — получить список (с пагинацией)
- `GET /api/performer/{performerId:guid}` — получить по ID
- `DELETE /api/performer/{performerId:guid}` — удалить

> Создание и обновление исполнителей (POST/PUT) пока не реализованы.

---

## Реальная структура БД (DbContracts)

База данных `DbContracts` на PostgreSQL (`postgresql-container`) содержит 3 основные таблицы:

### Таблицы

#### 1. **WorkUnits** (Единицы работ)
| Столбец | Тип | Описание |
|---------|-----|----------|
| `Id` | `smallint` | Первичный ключ (PK) |
| `Name` | `varchar(50)` | Название единицы работы (макс. 50 символов) |

**Примеры данных:**
- `1` — машина
- `2` — шт.
- `3` — Кв.м.

#### 2. **WorkTypes** (Виды работ)
| Столбец | Тип | Описание |
|---------|-----|----------|
| `Id` | `uuid` | Первичный ключ (PK) |
| `Name` | `varchar(200)` | Название вида работы (макс. 200 символов) |
| `WorkUnitId` | `smallint` | Внешний ключ (FK) → ссылается на `WorkUnits.Id` |

#### 3. **Performers** (Исполнители договоров)
| Столбец | Тип | Описание |
|---------|-----|----------|
| `Id` | `uuid` | Первичный ключ (PK) |
| `EntityId` | `uuid` | Идентификатор истории |
| `CreateDate` | `timestamp` | Дата создания |
| `EditDate` | `timestamp` | Дата редактирования (nullable) |
| `Creator` | `varchar(255)` | Создатель |
| `Editor` | `varchar(255)` | Редактор (nullable) |
| `Snils` | `char(11)` | СНИЛС |
| `Inn` | `char(12)` | ИНН (nullable) |
| `Surname` | `varchar(250)` | Фамилия |
| `Name` | `varchar(250)` | Имя |
| `Patronymic` | `varchar(250)` | Отчество |
| `Gender` | `integer` | Пол |
| `PassportSeries` | `char(4)` | Серия паспорта |
| `PassportNumber` | `char(6)` | Номер паспорта |
| `PassportIssuePlace` | `varchar(500)` | Место выдачи паспорта |
| `PassportIssueDate` | `timestamp` | Дата выдачи паспорта |
| `PassportDepartmentCode` | `char(6)` | Код подразделения |
| `PassportBirthPlace` | `varchar(500)` | Место рождения |
| `PassportBirthDate` | `timestamp` | Дата рождения |
| `PassportRegistrationPlace` | `varchar(500)` | Место регистрации |

### Схема связей
```
WorkUnits (1) ────< (M) WorkTypes
```
- **Тип связи:** Один ко многим (1:M).
- **Ограничение:** `FK_WorkTypes_WorkUnits`.
- **Логика:** Одна единица работы (`WorkUnit`) может использоваться в **многих** видах работ (`WorkType`). Поле `WorkUnitId` может быть NULL.

### Индексы
- `PK_WorkUnits` (btree, Id)
- `PK_WorkTypes` (btree, Id)
- `PK_Performers` (btree, Id)

---

## TODO / Известные места

- В `ContractGpdDbContext.OnConfiguring()` стоит `Debug.WriteLine` для логирования SQL-запросов (комментарий: *"для отладки, потом заменить на нормальное протоколирование"*)
- Строки подключения и чувствительные данные хранятся в User Secrets для dev-окружения
