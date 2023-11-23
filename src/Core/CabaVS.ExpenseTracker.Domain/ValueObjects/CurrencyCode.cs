using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.ValueObjects;

public sealed class CurrencyCode : ValueObject
{
    public const int MaxLength = 4;
    
    public string Value { get; }

    private CurrencyCode(string value)
    {
        Value = value;
    }

    public static Result<CurrencyCode> Create(string value)
    {
        var sanitizedValue = value.Trim();

        if (string.IsNullOrEmpty(sanitizedValue))
        {
            return CurrencyCodeErrors.Empty();
        }

        if (sanitizedValue.Length > MaxLength)
        {
            return CurrencyCodeErrors.TooLong(sanitizedValue.Length);
        }

        return new CurrencyCode(sanitizedValue);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}