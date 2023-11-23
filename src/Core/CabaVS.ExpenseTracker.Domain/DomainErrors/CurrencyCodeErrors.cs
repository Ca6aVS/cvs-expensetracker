using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.DomainErrors;

public static class CurrencyCodeErrors
{
    public static Error Empty() => new(
        "CurrencyCode.Empty", 
        "Currency Code is NULL or empty.");
    public static Error TooLong(int length) => new(
        "CurrencyCode.TooLong", 
        $"Currency Code is too long. Attempted '{length}' characters when maximum is '{CurrencyCode.MaxLength}'.");
}