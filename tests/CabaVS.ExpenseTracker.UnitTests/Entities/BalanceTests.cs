using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Entities;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.Entities;

public class BalanceTests
{
    [Fact]
    public void Balance_Should_BeCreated_WhenAllValuesAreValid()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        const string name = "Card of My Bank";
        const decimal amount = 10000m;
        var currency = Currency.Create(
            Guid.NewGuid(), 
            CurrencyName.Create("Polish zloty").Value,
            "PLN",
            "zl",
            true).Value;
        
        // Act
        var result = Balance.Create(id, name, amount, currency);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Value.Should().Be(name);
        result.Value.Amount.Should().Be(amount);
        result.Value.Currency.Should().Be(currency);
    }
    
    [Fact]
    public void Balance_ShouldNot_BeCreated_WhenNameIsEmpty()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        const string name = "    ";
        const decimal amount = 10000m;
        var currency = Currency.Create(
            Guid.NewGuid(), 
            CurrencyName.Create("Polish zloty").Value,
            "PLN",
            "zl",
            true).Value;
        
        // Act
        var result = Balance.Create(id, name, amount, currency);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BalanceNameErrors.Empty());
    }
    
    [Fact]
    public void Balance_ShouldNot_BeCreated_WhenNameIsTooLong()
    {
        // Arrange
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        const string name = "Unrealistically long Balance Name." +
                            "Unrealistically long Balance Name." +
                            "Unrealistically long Balance Name." +
                            "Unrealistically long Balance Name.";
        const decimal amount = 10000m;
        var currency = Currency.Create(
            Guid.NewGuid(), 
            CurrencyName.Create("Polish zloty").Value,
            "PLN",
            "zl",
            true).Value;
        
        // Act
        var result = Balance.Create(id, name, amount, currency);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BalanceNameErrors.TooLong(name.Length));
    }
}