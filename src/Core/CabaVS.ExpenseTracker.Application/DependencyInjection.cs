using Microsoft.Extensions.DependencyInjection;

namespace CabaVS.ExpenseTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(ApplicationAssemblyMarker.Assembly);
                config.Lifetime = ServiceLifetime.Scoped;
            });
    }
}