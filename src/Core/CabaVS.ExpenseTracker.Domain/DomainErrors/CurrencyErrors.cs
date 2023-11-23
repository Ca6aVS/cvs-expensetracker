using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.DomainErrors;

public static class CurrencyErrors
{
    public static Error NameTaken(CurrencyName name) => new(
        "Currency.NameAlreadyTaken", 
        $"Currency already exists with Name '{name.Value}'.");
}