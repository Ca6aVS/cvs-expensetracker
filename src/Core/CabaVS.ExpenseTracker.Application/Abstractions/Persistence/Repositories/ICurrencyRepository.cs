using CabaVS.ExpenseTracker.Domain.Entities;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Application.Abstractions.Persistence.Repositories;

public interface ICurrencyRepository
{
    Task<bool> IsExistsByName(CurrencyName name, CancellationToken ct = default);
    Task<Currency?> GetById(Guid id, CancellationToken ct = default);
    Task Create(Currency currency, CancellationToken ct = default);
    Task Update(Currency currency, CancellationToken ct = default);
}