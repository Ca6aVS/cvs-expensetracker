using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.DomainErrors;

public static class CurrencyNameErrors
{
    public static Error Empty() => new(
        "CurrencyName.Empty", 
        "Currency Name is NULL or empty.");
    public static Error TooLong(int length) => new(
        "CurrencyName.TooLong", 
        $"Currency Name is too long. Attempted '{length}' characters when maximum is '{CurrencyName.MaxLength}'.");
}