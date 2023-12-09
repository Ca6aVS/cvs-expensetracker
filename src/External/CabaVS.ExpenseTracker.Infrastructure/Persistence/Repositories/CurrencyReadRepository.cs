using CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;
using CabaVS.ExpenseTracker.Application.Features.Currencies.Models;
using Dapper;

namespace CabaVS.ExpenseTracker.Infrastructure.Persistence.Repositories;

internal sealed class CurrencyReadRepository : ICurrencyReadRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public CurrencyReadRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task<CurrencyModel[]> GetAll(CancellationToken ct = default)
    {
        using var connection = _dbConnectionFactory.Build();
        
        var allCurrencies = await connection.QueryAsync<CurrencyModel>(
            CurrencyModelSelectSql);
        
        return allCurrencies.ToArray();
    }

    public async Task<CurrencyModel?> GetById(Guid id, CancellationToken ct = default)
    {
        using var connection = _dbConnectionFactory.Build();
        
        var currency = await connection.QueryFirstOrDefaultAsync<CurrencyModel>(
            CurrencyModelSelectByIdSql, 
            new { CurrencyId = id });
        
        return currency;
    }

    private const string CurrencyModelSelectSql = 
        "SELECT [Id], [Name], [Code], [Symbol] FROM [dbo].[Currencies]";
    private const string CurrencyModelSelectByIdSql = 
        """
        SELECT [Id], [Name], [Code], [Symbol] FROM [dbo].[Currencies]
        WHERE [Id] = @CurrencyId
        """;
}