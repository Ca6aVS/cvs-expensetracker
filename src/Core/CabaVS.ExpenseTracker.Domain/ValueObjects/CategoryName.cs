using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.ValueObjects;

public sealed class CategoryName : ValueObject
{
    public const int MaxLength = 64;
    
    public string Value { get; }

    private CategoryName(string value)
    {
        Value = value;
    }

    public static Result<CategoryName> Create(string value)
    {
        var sanitizedValue = value.Trim();

        if (string.IsNullOrEmpty(sanitizedValue))
        {
            return CategoryNameErrors.Empty();
        }

        if (sanitizedValue.Length > MaxLength)
        {
            return CategoryNameErrors.TooLong(sanitizedValue.Length);
        }

        return new CategoryName(sanitizedValue);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}