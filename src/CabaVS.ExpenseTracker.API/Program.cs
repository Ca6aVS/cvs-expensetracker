using CabaVS.ExpenseTracker.Application;
using CabaVS.ExpenseTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddApplication()
    .AddPersistence(configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();