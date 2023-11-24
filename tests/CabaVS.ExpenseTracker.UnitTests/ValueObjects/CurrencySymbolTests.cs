using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.ValueObjects;

public class CurrencySymbolTests
{
    [Fact]
    public void CurrencySymbol_Should_BeCreated_WhenValueIsValid()
    {
        // Arrange
        const string currencySymbol = "$";
        
        // Act
        var result = CurrencySymbol.Create(currencySymbol);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(currencySymbol);
    }
    
    [Fact]
    public void CurrencySymbol_ShouldNot_BeCreated_WhenValueIsEmpty()
    {
        // Arrange
        const string currencySymbol = "  ";
        
        // Act
        var result = CurrencySymbol.Create(currencySymbol);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencySymbolErrors.Empty());
    }
    
    [Fact]
    public void CurrencySymbol_ShouldNot_BeCreated_WhenValueIsTooLong()
    {
        // Arrange
        const string currencySymbol = "Unrealistically long Currency Symbol.";
        
        // Act
        var result = CurrencySymbol.Create(currencySymbol);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencySymbolErrors.TooLong(currencySymbol.Length));
    }
    
    [Fact]
    public void CurrencySymbol_Should_BeEqual_WhenValuesAreSame()
    {
        // Arrange
        const string value = "$";

        var one = CurrencySymbol.Create(value).Value;
        var two = CurrencySymbol.Create(value).Value;

        var oneAsObj = (object)one;
        var twoAsObj = (object)two;
        
        // Act
        var equalsByMethod = one.Equals(two);
        var equalsByMethodAsObject = oneAsObj.Equals(twoAsObj);
        var equalsByHashCode = one.GetHashCode() == two.GetHashCode();
        var equalsByOperator = one == two;
        var notEqualsByOperator = one != two;
        
        // Assert
        equalsByMethod.Should().BeTrue();
        equalsByMethodAsObject.Should().BeTrue();
        equalsByHashCode.Should().BeTrue();
        equalsByOperator.Should().BeTrue();
        notEqualsByOperator.Should().BeFalse();
    }
}