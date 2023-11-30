using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CabaVS.ExpenseTracker.Infrastructure.Persistence;

internal interface IDbConnectionFactory
{
    IDbConnection Build();
}

internal sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly PersistenceOptions _persistenceOptions;
    
    public DbConnectionFactory(IOptions<PersistenceOptions> persistenceOptions)
    {
        _persistenceOptions = persistenceOptions.Value;
    }

    public IDbConnection Build() => new SqlConnection(_persistenceOptions.DbConnectionString);
}