using CabaVS.ExpenseTracker.Domain.Primitives;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.Primitives;

public class ValueObjectTests
{
    [Fact]
    public void ValueObject_Should_BeEqual_WhenValuesAndTypesAreSame()
    {
        // Arrange
        const string value = "Value";

        var valueObjectOne = new ValueObjectOne(value);
        var valueObjectTwo = new ValueObjectOne(value);

        var valueObjectOneAsObject = (object)valueObjectOne;
        var valueObjectTwoAsObject = (object)valueObjectTwo;
        
        // Act
        var equalsByMethod = valueObjectOne.Equals(valueObjectTwo);
        var equalsByMethodAsObj = valueObjectOneAsObject.Equals(valueObjectTwoAsObject);
        var equalsByHashCode = valueObjectOne.GetHashCode() == valueObjectTwo.GetHashCode();
        var equalsByOperator = valueObjectOne == valueObjectTwo;
        var notEqualsByOperator = valueObjectOne != valueObjectTwo;
        
        // Assert
        equalsByMethod.Should().BeTrue();
        equalsByMethodAsObj.Should().BeTrue();
        equalsByHashCode.Should().BeTrue();
        equalsByOperator.Should().BeTrue();
        notEqualsByOperator.Should().BeFalse();
    }
    
    [Fact]
    public void ValueObject_ShouldNot_BeEqual_WhenTypesAreDifferent()
    {
        // Arrange
        const string value = "Value";

        var valueObjectOne = new ValueObjectOne(value);
        var valueObjectTwo = new ValueObjectTwo(value);

        var valueObjectOneAsObject = (object)valueObjectOne;
        var valueObjectTwoAsObject = (object)valueObjectTwo;
        
        // Act
        var equalsByMethod = valueObjectOne.Equals(valueObjectTwo);
        var equalsByMethodAsObj = valueObjectOneAsObject.Equals(valueObjectTwoAsObject);
        var equalsByHashCode = valueObjectOne.GetHashCode() == valueObjectTwo.GetHashCode();
        var equalsByOperator = valueObjectOne == valueObjectTwo;
        var notEqualsByOperator = valueObjectOne != valueObjectTwo;
        
        // Assert
        equalsByMethod.Should().BeFalse();
        equalsByMethodAsObj.Should().BeFalse();
        equalsByHashCode.Should().BeFalse();
        equalsByOperator.Should().BeFalse();
        notEqualsByOperator.Should().BeTrue();
    }
    
    [Fact]
    public void ValueObject_ShouldNot_BeEqual_WhenValuesAreDifferent()
    {
        // Arrange
        const string value = "Value";

        var valueObjectOne = new ValueObjectOne(value);
        var valueObjectTwo = new ValueObjectTwo(value);

        var valueObjectOneAsObject = (object)valueObjectOne;
        var valueObjectTwoAsObject = (object)valueObjectTwo;
        
        // Act
        var equalsByMethod = valueObjectOne.Equals(valueObjectTwo);
        var equalsByMethodAsObj = valueObjectOneAsObject.Equals(valueObjectTwoAsObject);
        var equalsByHashCode = valueObjectOne.GetHashCode() == valueObjectTwo.GetHashCode();
        var equalsByOperator = valueObjectOne == valueObjectTwo;
        var notEqualsByOperator = valueObjectOne != valueObjectTwo;
        
        // Assert
        equalsByMethod.Should().BeFalse();
        equalsByMethodAsObj.Should().BeFalse();
        equalsByHashCode.Should().BeFalse();
        equalsByOperator.Should().BeFalse();
        notEqualsByOperator.Should().BeTrue();
    }
    
    private class ValueObjectOne : ValueObject
    {
        public string Value { get; }
        
        public ValueObjectOne(string value)
        {
            Value = value;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
    
    private class ValueObjectTwo : ValueObject
    {
        public string Value { get; }
        
        public ValueObjectTwo(string value)
        {
            Value = value;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}