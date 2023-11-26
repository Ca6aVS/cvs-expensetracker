using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Entities;
using CabaVS.ExpenseTracker.Domain.Enums;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.Entities;

public class CategoryTests
{
    [Fact]
    public void Category_Should_BeCreated_WhenAllValuesAreValid()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        const string name = "Groceries";
        const CategoryType type = CategoryType.Expense;
        var currency = Currency.Create(
            Guid.NewGuid(), 
            CurrencyName.Create("Polish zloty").Value,
            "PLN",
            "zl",
            true).Value;
        
        // Act
        var result = Category.Create(id, name, type, currency);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Value.Should().Be(name);
        result.Value.Type.Should().Be(type);
        result.Value.Currency.Should().Be(currency);
    }
    
    [Fact]
    public void Category_ShouldNot_BeCreated_WhenNameIsEmpty()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        const string name = "    ";
        const CategoryType type = CategoryType.Expense;
        var currency = Currency.Create(
            Guid.NewGuid(), 
            CurrencyName.Create("Polish zloty").Value,
            "PLN",
            "zl",
            true).Value;
        
        // Act
        var result = Category.Create(id, name, type, currency);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CategoryNameErrors.Empty());
    }
    
    [Fact]
    public void Category_ShouldNot_BeCreated_WhenNameIsTooLong()
    {
        // Arrange
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        const string name = "Unrealistically long Category Name." +
                            "Unrealistically long Category Name." +
                            "Unrealistically long Category Name." +
                            "Unrealistically long Category Name.";
        const CategoryType type = CategoryType.Expense;
        var currency = Currency.Create(
            Guid.NewGuid(), 
            CurrencyName.Create("Polish zloty").Value,
            "PLN",
            "zl",
            true).Value;
        
        // Act
        var result = Category.Create(id, name, type, currency);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CategoryNameErrors.TooLong(name.Length));
    }
}