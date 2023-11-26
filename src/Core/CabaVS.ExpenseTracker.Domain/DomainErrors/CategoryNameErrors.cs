using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.DomainErrors;

public static class CategoryNameErrors
{
    public static Error Empty() => new(
        "CategoryName.Empty", 
        "Category Name is NULL or empty.");
    public static Error TooLong(int length) => new(
        "CategoryName.TooLong", 
        $"Category Name is too long. Attempted '{length}' characters when maximum is '{CategoryName.MaxLength}'.");
}