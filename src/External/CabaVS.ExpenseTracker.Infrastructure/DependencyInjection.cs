using CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;
using CabaVS.ExpenseTracker.Infrastructure.Persistence;
using CabaVS.ExpenseTracker.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CabaVS.ExpenseTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection,
        IConfiguration configuration, string sectionKey = "Persistence")
    {
        serviceCollection
            .Configure<PersistenceOptions>(
                options => configuration.GetSection(sectionKey).Bind(options));

        serviceCollection.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        serviceCollection.AddScoped<ICurrencyReadRepository, CurrencyReadRepository>();
        
        var persistenceOptions = configuration.GetSection(sectionKey).Get<PersistenceOptions>()
                                 ?? throw new InvalidOperationException($"Not configured '{nameof(PersistenceOptions)}'."); 
        serviceCollection
            .AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(persistenceOptions.DbConnectionString),
                contextLifetime: ServiceLifetime.Transient);

        return serviceCollection;
    }
}