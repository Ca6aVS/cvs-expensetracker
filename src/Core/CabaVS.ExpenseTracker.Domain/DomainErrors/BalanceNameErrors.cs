using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.DomainErrors;

public static class BalanceNameErrors
{
    public static Error Empty() => new(
        "BalanceName.Empty", 
        "Balance Name is NULL or empty.");
    public static Error TooLong(int length) => new(
        "BalanceName.TooLong", 
        $"Balance Name is too long. Attempted '{length}' characters when maximum is '{BalanceName.MaxLength}'.");
}