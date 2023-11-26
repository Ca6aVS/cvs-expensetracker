using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.ValueObjects;

public sealed class BalanceName : ValueObject
{
    public const int MaxLength = 64;
    
    public string Value { get; }

    private BalanceName(string value)
    {
        Value = value;
    }

    public static Result<BalanceName> Create(string value)
    {
        var sanitizedValue = value.Trim();

        if (string.IsNullOrEmpty(sanitizedValue))
        {
            return BalanceNameErrors.Empty();
        }

        if (sanitizedValue.Length > MaxLength)
        {
            return BalanceNameErrors.TooLong(sanitizedValue.Length);
        }

        return new BalanceName(sanitizedValue);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}