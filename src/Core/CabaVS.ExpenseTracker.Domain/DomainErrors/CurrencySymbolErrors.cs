using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.DomainErrors;

public static class CurrencySymbolErrors
{
    public static Error Empty() => new(
        "CurrencySymbol.Empty", 
        "Currency Symbol is NULL or empty.");
    public static Error TooLong(int length) => new(
        "CurrencySymbol.TooLong", 
        $"Currency Symbol is too long. Attempted '{length}' characters when maximum is '{CurrencySymbol.MaxLength}'.");
}