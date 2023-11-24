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

    [Fact]
    public void CurrencyCode_Should_BeEqual_WhenValuesAreSame()
    {
        // Arrange
        const string value = "USD";

        var one = CurrencyCode.Create(value).Value;
        var two = CurrencyCode.Create(value).Value;

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