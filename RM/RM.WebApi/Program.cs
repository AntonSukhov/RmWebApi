using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RM.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run();