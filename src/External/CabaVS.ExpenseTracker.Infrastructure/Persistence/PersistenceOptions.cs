namespace CabaVS.ExpenseTracker.Infrastructure.Persistence;

internal sealed class PersistenceOptions
{
    public required string DbConnectionString { get; init; }
}