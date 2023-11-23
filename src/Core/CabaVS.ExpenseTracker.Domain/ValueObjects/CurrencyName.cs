using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.ValueObjects;

public sealed class CurrencyName : ValueObject
{
    public const int MaxLength = 64;
    
    public string Value { get; }

    private CurrencyName(string value)
    {
        Value = value;
    }

    public static Result<CurrencyName> Create(string value)
    {
        var sanitizedValue = value.Trim();

        if (string.IsNullOrEmpty(sanitizedValue))
        {
            return CurrencyNameErrors.Empty();
        }

        if (sanitizedValue.Length > MaxLength)
        {
            return CurrencyNameErrors.TooLong(sanitizedValue.Length);
        }

        return new CurrencyName(sanitizedValue);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}