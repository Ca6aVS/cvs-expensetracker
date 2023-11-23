using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.ValueObjects;

public class CurrencyCodeTests
{
    [Fact]
    public void CurrencyCode_Should_BeCreated_WhenValueIsValid()
    {
        // Arrange
        const string currencyCode = "USD";
        
        // Act
        var result = CurrencyCode.Create(currencyCode);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(currencyCode);
    }
    
    [Fact]
    public void CurrencyCode_ShouldNot_BeCreated_WhenValueIsEmpty()
    {
        // Arrange
        const string currencyCode = "   ";
        
        // Act
        var result = CurrencyCode.Create(currencyCode);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencyCodeErrors.Empty());
    }
    
    [Fact]
    public void CurrencyCode_ShouldNot_BeCreated_WhenValueIsTooLong()
    {
        // Arrange
        const string currencyCode = "Unrealistically long Currency Code.";
        
        // Act
        var result = CurrencyCode.Create(currencyCode);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencyCodeErrors.TooLong(currencyCode.Length));
    }
}