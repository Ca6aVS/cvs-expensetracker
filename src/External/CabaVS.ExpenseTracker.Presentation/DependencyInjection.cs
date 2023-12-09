using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CabaVS.ExpenseTracker.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection serviceCollection, IWebHostEnvironment environment)
    {
        serviceCollection.AddFastEndpoints();

        if (environment.IsDevelopment())
        {
            serviceCollection.SwaggerDocument(options =>
            {
                options.DocumentSettings = settings =>
                {
                    settings.Title = "CVS - Expense Tracker [API]";
                    settings.Version = "v1";
                };
                options.EnableJWTBearerAuth = false;
                options.AutoTagPathSegmentIndex = 2;
            });
            
            serviceCollection.AddEndpointsApiExplorer();
        }

        return serviceCollection;
    }
}