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
    
    [Fact]
    public void CurrencyName_Should_BeEqual_WhenValuesAreSame()
    {
        // Arrange
        const string value = "Polish zloty";

        var one = CurrencyName.Create(value).Value;
        var two = CurrencyName.Create(value).Value;

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