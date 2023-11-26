using CabaVS.ExpenseTracker.Domain.Enums;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;
using CabaVS.ExpenseTracker.Domain.ValueObjects;

namespace CabaVS.ExpenseTracker.Domain.Entities;

public sealed class Category : Entity
{
    public CategoryName Name { get; set; }
    public CategoryType Type { get; }
    public Currency Currency { get; }
    
    private Category(Guid id, CategoryName name, CategoryType type, Currency currency) : base(id)
    {
        Name = name;
        Type = type;
        Currency = currency;
    }
    
    public static Result<Category> Create(Guid id, string name, CategoryType type, Currency currency)
    {
        var nameResult = CategoryName.Create(name);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        return new Category(id, nameResult.Value, type, currency);
    }
}