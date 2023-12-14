using CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using CabaVS.ExpenseTracker.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabaVS.ExpenseTracker.Infrastructure.Persistence.Repositories;

internal sealed class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CurrencyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsExistsByName(CurrencyName name, CancellationToken ct = default)
    {
        var isExists = await _dbContext.Currencies
            .AnyAsync(c => c.Name == name.Value, ct);
        return isExists;
    }

    public async Task Create(Domain.Entities.Currency currency, CancellationToken ct = default)
    {
        var currencyToCreate = Currency.FromDomainEntity(currency);

        await _dbContext.Currencies.AddAsync(currencyToCreate, ct);
    }
}