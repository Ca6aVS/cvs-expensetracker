using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.ValueObjects;

public class BalanceNameTests
{
    [Fact]
    public void BalanceName_Should_BeCreated_WhenValueIsValid()
    {
        // Arrange
        const string balanceName = "Groceries";
        
        // Act
        var result = BalanceName.Create(balanceName);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(balanceName);
    }
    
    [Fact]
    public void BalanceName_ShouldNot_BeCreated_WhenValueIsEmpty()
    {
        // Arrange
        const string balanceName = "    ";
        
        // Act
        var result = BalanceName.Create(balanceName);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BalanceNameErrors.Empty());
    }
    
    [Fact]
    public void BalanceName_ShouldNot_BeCreated_WhenValueIsTooLong()
    {
        // Arrange
        const string balanceName = "Unrealistically long Balance Name." +
                                    "Unrealistically long Balance Name." +
                                    "Unrealistically long Balance Name." +
                                    "Unrealistically long Balance Name.";
        
        // Act
        var result = BalanceName.Create(balanceName);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BalanceNameErrors.TooLong(balanceName.Length));
    }
    
    [Fact]
    public void BalanceName_Should_BeEqual_WhenValuesAreSame()
    {
        // Arrange
        const string value = "Groceries";

        var one = BalanceName.Create(value).Value;
        var two = BalanceName.Create(value).Value;

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