using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.ValueObjects;

public class CategoryNameTests
{
    [Fact]
    public void CategoryName_Should_BeCreated_WhenValueIsValid()
    {
        // Arrange
        const string categoryName = "Groceries";
        
        // Act
        var result = CategoryName.Create(categoryName);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(categoryName);
    }
    
    [Fact]
    public void CategoryName_ShouldNot_BeCreated_WhenValueIsEmpty()
    {
        // Arrange
        const string categoryName = "    ";
        
        // Act
        var result = CategoryName.Create(categoryName);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CategoryNameErrors.Empty());
    }
    
    [Fact]
    public void CategoryName_ShouldNot_BeCreated_WhenValueIsTooLong()
    {
        // Arrange
        const string categoryName = "Unrealistically long Category Name." +
                                    "Unrealistically long Category Name." +
                                    "Unrealistically long Category Name." +
                                    "Unrealistically long Category Name.";
        
        // Act
        var result = CategoryName.Create(categoryName);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CategoryNameErrors.TooLong(categoryName.Length));
    }
    
    [Fact]
    public void CategoryName_Should_BeEqual_WhenValuesAreSame()
    {
        // Arrange
        const string value = "Groceries";

        var one = CategoryName.Create(value).Value;
        var two = CategoryName.Create(value).Value;

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