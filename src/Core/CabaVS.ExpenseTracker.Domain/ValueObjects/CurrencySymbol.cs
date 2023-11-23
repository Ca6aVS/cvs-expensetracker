using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.ValueObjects;

public sealed class CurrencySymbol : ValueObject
{
    public const int MaxLength = 4;
    
    public string Value { get; }

    private CurrencySymbol(string value)
    {
        Value = value;
    }

    public static Result<CurrencySymbol> Create(string value)
    {
        var sanitizedValue = value.Trim();

        if (string.IsNullOrEmpty(sanitizedValue))
        {
            return CurrencySymbolErrors.Empty();
        }

        if (sanitizedValue.Length > MaxLength)
        {
            return CurrencySymbolErrors.TooLong(sanitizedValue.Length);
        }

        return new CurrencySymbol(sanitizedValue);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}