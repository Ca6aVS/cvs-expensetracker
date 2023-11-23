using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.ValueObjects;

public class CurrencyNameTests
{
    [Fact]
    public void CurrencyName_Should_BeCreated_WhenValueIsValid()
    {
        // Arrange
        const string currencyName = "Polish zloty";
        
        // Act
        var result = CurrencyName.Create(currencyName);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(currencyName);
    }
    
    [Fact]
    public void CurrencyName_ShouldNot_BeCreated_WhenValueIsEmpty()
    {
        // Arrange
        const string currencyName = "    ";
        
        // Act
        var result = CurrencyName.Create(currencyName);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencyNameErrors.Empty());
    }
    
    [Fact]
    public void CurrencyName_ShouldNot_BeCreated_WhenValueIsTooLong()
    {
        // Arrange
        const string currencyName = "Unrealistically long Currency Name." +
                                    "Unrealistically long Currency Name." +
                                    "Unrealistically long Currency Name." +
                                    "Unrealistically long Currency Name.";
        
        // Act
        var result = CurrencyName.Create(currencyName);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencyNameErrors.TooLong(currencyName.Length));
    }
}