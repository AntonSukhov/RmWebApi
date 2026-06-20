## About

[](#about)

The RM.Api package provides a comprehensive set of tools for interacting with the RM Web API. It offers a clean and modern API for handling work types, work units, and other modules through typed HTTP clients.

## How to Use

[](#how-to-use)

## Using the WorkTypeApiService and WorkUnitApiService Classes

[](#using-the-worktypeapiservice-and-workunitapiservice-classes)

The `WorkTypeApiService` and `WorkUnitApiService` classes are used to interact with the RM Web API endpoints. They encapsulate:

* Correct creation and configuration of `HttpClient`;
* Serialization/deserialization of requests and responses;
* Communication with RM Web API REST endpoints;
* Mapping between DTOs and generated API client models.

**Important:** The `WorkTypeApiService` and `WorkUnitApiService` must be created via dependency injection or with a properly configured `IHttpClientFactory`.

### Example for Regular Applications (Manual Setup)

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

// Create WorkTypeApiService instance
var workTypeService = new WorkTypeApiService(httpClientFactory);

// Create WorkUnitApiService instance
var workUnitService = new WorkUnitApiService(httpClientFactory);

// Perform operations
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

## Using the ServiceCollectionExtensions Class

[](#using-the-servicecollectionextensions-class)

For web applications, use the ServiceCollectionExtensions class to register the API services with the DI container.

### Example for Web Applications (ASP.NET Core)

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

## API Client Generation

[](#api-client-generation)

The generated API client (`RmWebApiClient`) is created using NSwag from the OpenAPI specification. To regenerate the client:

```bash
cd RM/RM.Api/Scripts
./generate-client.sh
```

This will:
1. Restore .NET tools
2. Generate the client from `OpenApiSpecs/RmWebApi.v1.json`
3. Output to `GeneratedApiClients/RmWebApiClient.cs`

## Main Types

[](#main-types)

The main types provided by this library are:

* `RM.Api.Services.IWorkTypeApiService` - Interface for work type operations
* `RM.Api.Services.WorkTypeApiService` - Implementation for work type operations
* `RM.Api.Services.IWorkUnitApiService` - Interface for work unit operations
* `RM.Api.Services.WorkUnitApiService` - Implementation for work unit operations
* `RM.Api.DTOs.Requests.WorkTypeCreationRequest` - Work type creation request DTO
* `RM.Api.DTOs.Requests.WorkTypeUpdationRequest` - Work type update request DTO
* `RM.Api.DTOs.Requests.PageOptionsRequest` - Pagination options request DTO
* `RM.Api.DTOs.Responses.WorkTypeResponse` - Work type response DTO
* `RM.Api.DTOs.Responses.WorkUnitResponse` - Work unit response DTO
* `RM.Api.Mapping.Extensions.WorkTypeMapper` - Work type mapping extensions
* `RM.Api.Mapping.Extensions.WorkUnitMapper` - Work unit mapping extensions
* `RM.Api.GeneratedApiClients.RmWebApiClient` - Generated typed HTTP client
* `RM.Common.Constants.ApiConstants` - API-related constants

## Project Structure

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

## Feedback & Contributing

[](#feedback--contributing)

RM.Api is released as open source under the MIT license. Bug reports, feature requests, and contributions are welcome at the GitHub repository.
