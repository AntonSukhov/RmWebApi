# 📦 RM.Api

> A client library for interacting with **RM Web API**. Provides typed HTTP clients for working with work types, work units, contract performers, and other modules.

---

## 📋 Table of Contents

- [About](#about)
- [Quick Start](#how-to-use)
  - [Manual Setup](#example-for-regular-applications-manual-setup)
  - [ASP.NET Core (DI)](#example-for-web-applications-aspnet-core)
- [API Client Generation](#api-client-generation)
- [Main Types](#main-types)
- [Project Structure](#project-structure)

---

## 📖 About

[](#about)

The RM.Api package provides a comprehensive set of tools for interacting with the RM Web API. It offers a clean and modern API for handling work types, work units, and other modules through typed HTTP clients.

---

## 🚀 How to Use

[](#how-to-use)

### 📚 Using the API Service Classes

[](#using-the-api-service-classes)

The `WorkTypeService`, `WorkUnitService`, and `PerformerService` classes are used to interact with the RM Web API endpoints. They encapsulate:

| Feature | Description |
|---------|-------------|
| `HttpClient` | Correct creation and configuration of HTTP client |
| Serialization | Automatic serialization/deserialization of requests and responses |
| REST API | Communication with RM Web API REST endpoints |
| Mapping | Conversion between DTOs and generated API client models |

> ⚠️ **Important:** The services must be created via dependency injection or with a properly configured `IHttpClientFactory`.

---

### 🛠 Example for Regular Applications (Manual Setup)

[](#example-for-regular-applications-manual-setup)

```csharp
using RM.Api.Services;
using RM.Api.Mapping.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using RM.Common.Constants;

// Create service collection and HttpClientFactory
var services = new ServiceCollection();

services.AddHttpClient(ApiConstants.RmWebApiClientName, client =>
{
    client.BaseAddress = new Uri("https://localhost:7121/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

var serviceProvider = services.BuildServiceProvider();
var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

// Create services instances
var workTypeService = new WorkTypeService(httpClientFactory);
var workUnitService = new WorkUnitService(httpClientFactory);
var performerService = new PerformerService(httpClientFactory);

// Execute operations
try
{
    // Get all work types
    var workTypes = await workTypeService.GetAllAsync(new PageOptionsRequest
    {
        PageNumber = 1,
        PageSize = 10
    });

    // Get all work units
    var workUnits = await workUnitService.GetAllAsync();

    // Get all performers with pagination
    var performers = await performerService.GetAllAsync(new PageOptionsRequest
    {
        PageNumber = 1,
        PageSize = 10
    });

    // Get a single performer by ID
    var performerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479");
    var performer = await performerService.GetByIdAsync(performerId);

    if (performer != null)
    {
        Console.WriteLine($"Performer: {performer.Surname} {performer.Name} {performer.Patronymic}");
        Console.WriteLine($"SNILS: {performer.Snils}, INN: {performer.Inn}");
        Console.WriteLine($"Passport: {performer.PassportSeries} {performer.PassportNumber}");
    }

    // Delete a performer by ID
    await performerService.DeleteAsync(performerId);
    Console.WriteLine($"Deleted performer with ID: {performerId}");

    // Create work type
    var workTypeId = await workTypeService.CreateAsync(new WorkTypeCreationRequest
    {
        Name = "Development",
        WorkUnitId = 1
    });

    Console.WriteLine($"Created work type with ID: {workTypeId}");
}
catch (Exception ex)
{
    Console.WriteLine($"API call failed: {ex.Message}");
}
```

---

### 🔌 Example for Web Applications (ASP.NET Core)

[](#using-the-servicecollectionextensions-class)

For web applications, use the `ServiceCollectionExtensions` class to register the API services with the DI container.

[](#example-for-web-applications-aspnet-core)

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add RM API services with default Scoped lifetime
builder.Services.AddRmApiServices();

// Or specify different service lifetime:

// Transient
builder.Services.AddRmApiServices(
    ServiceLifetime.Transient
);

// Singleton
builder.Services.AddRmApiServices(
    ServiceLifetime.Singleton
);
```

---

## 🔄 API Client Generation

[](#api-client-generation)

The generated API client (`RmWebApiClient`) is created using **NSwag** from the OpenAPI specification. To regenerate the client:

```bash
cd RM/RM.Api/Scripts
./generate-client.sh
```

**What the script does:**

1. 📦 Restores .NET tools
2. 🏗 Generates the client from `OpenApiSpecs/RmWebApi.v1.json`
3. 📄 Output to `GeneratedApiClients/RmWebApiClient.cs`

---

## 📦 Main Types

[](#main-types)

The main types provided by this library are:

### 🔧 Services

| Type | Description |
|------|-------------|
| `RM.Api.Services.IWorkTypeService` | Interface for work type operations |
| `RM.Api.Services.WorkTypeService` | Implementation for work types |
| `RM.Api.Services.IWorkUnitService` | Interface for work unit operations |
| `RM.Api.Services.WorkUnitService` | Implementation for work units |
| `RM.Api.Services.IPerformerService` | Interface for performer operations |
| `RM.Api.Services.PerformerService` | Implementation for performers |

#### 📋 Service Methods

<table>
  <thead>
    <tr>
      <th>Service</th>
      <th>Method</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td rowspan="5"><code>WorkTypeService</code></td>
      <td><code>GetAllAsync</code></td>
      <td>Gets all work types with pagination</td>
    </tr>
    <tr>
      <td><code>GetByIdAsync</code></td>
      <td>Gets a work type by ID</td>
    </tr>
    <tr>
      <td><code>CreateAsync</code></td>
      <td>Creates a work type</td>
    </tr>
    <tr>
      <td><code>DeleteAsync</code></td>
      <td>Deletes a work type by ID</td>
    </tr>
    <tr>
      <td><code>UpdateAsync</code></td>
      <td>Updates a work type</td>
    </tr>
    <tr>
      <td><code>WorkUnitService</code></td>
      <td><code>GetAllAsync</code></td>
      <td>Gets all work units</td>
    </tr>
    <tr>
      <td rowspan="3"><code>PerformerService</code></td>
      <td><code>GetAllAsync</code></td>
      <td>Gets all contract performers with pagination</td>
    </tr>
    <tr>
      <td><code>GetByIdAsync</code></td>
      <td>Gets a contract performer by ID</td>
    </tr>
    <tr>
      <td><code>DeleteAsync</code></td>
      <td>Deletes a contract performer by ID</td>
    </tr>
  </tbody>
</table>

> 💡 **Note:** All methods accept an optional `CancellationToken?` parameter. For detailed signatures and XML documentation, see the corresponding interfaces (`IWorkTypeService`, `IWorkUnitService`, `IPerformerService`) in your IDE.

### 📥 Requests

| Type | Description |
|------|-------------|
| `RM.Api.DTOs.Requests.WorkTypeCreationRequest` | DTO for creating a work type |
| `RM.Api.DTOs.Requests.WorkTypeUpdationRequest` | DTO for updating a work type |
| `RM.Api.DTOs.Requests.PageOptionsRequest` | DTO for pagination options |

### 📤 Responses

| Type | Description |
|------|-------------|
| `RM.Api.DTOs.Responses.WorkTypeResponse` | Work type response DTO |
| `RM.Api.DTOs.Responses.WorkUnitResponse` | Work unit response DTO |
| `RM.Api.DTOs.Responses.PerformerResponse` | Performer response DTO |

### 🗺 Mappers

| Type | Description |
|------|-------------|
| `RM.Api.Mapping.Extensions.WorkTypeMapper` | Mapper for work types |
| `RM.Api.Mapping.Extensions.WorkUnitMapper` | Mapper for work units |
| `RM.Api.Mapping.Extensions.PerformerMapper` | Mapper for performers |

### 🏗 Others

| Type | Description |
|------|-------------|
| `RM.Api.GeneratedApiClients.RmWebApiClient` | Generated typed HTTP client |
| `RM.Common.Constants.ApiConstants` | API-related constants |

---

## 📁 Project Structure

[](#project-structure)

```
RM.Api/
├── DTOs/
│   ├── Requests/           # Request DTOs
│   └── Responses/          # Response DTOs
├── GeneratedApiClients/    # NSwag generated clients
├── Mapping/
│   └── Extensions/         # Mapper extensions
├── Services/               # API service implementations
├── Scripts/                # Client generation scripts
└── OpenApiSpecs/           # OpenAPI specifications
```

---

## 💬 Feedback & Contributing

[](#feedback--contributing)

RM.Api is released as open source under the [MIT license](https://opensource.org/licenses/MIT). Bug reports, feature requests, and contributions are welcome.

---

*Generated for **RM Web API** • .NET 9*
