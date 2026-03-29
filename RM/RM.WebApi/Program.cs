using Microsoft.AspNetCore.Builder;
using RM.WebApi;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run();