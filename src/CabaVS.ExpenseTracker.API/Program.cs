using CabaVS.ExpenseTracker.Application;
using CabaVS.ExpenseTracker.Infrastructure;
using CabaVS.ExpenseTracker.Presentation;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

builder.Services
    .AddApplication()
    .AddPersistence(configuration)
    .AddPresentation(environment);

var app = builder.Build();

app.UseFastEndpoints();

if (environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config => config.ConfigureDefaults());
}

app.Run();