using CabaVS.ExpenseTracker.Domain.Primitives;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.Primitives;

public class EntityTests
{
    [Fact]
    public void Entity_Should_BeEqual_WhenIdsAndTypesAreSame()
    {
        // Arrange
        var id = Guid.NewGuid();

        var entityOne = new DummyEntityOne(id, "Prop #1", 1);
        var entityTwo = new DummyEntityOne(id, "Prop #2", 2);

        var entityOneAsObj = (object)entityOne;
        var entityTwoAsObj = (object)entityTwo;
        
        // Act
        var equalsByMethod = entityOne.Equals(entityTwo);
        var equalsByMethodAsObj = entityOneAsObj.Equals(entityTwoAsObj);
        var equalsByHashCode = entityOne.GetHashCode() == entityTwo.GetHashCode();
        var equalsByOperator = entityOne == entityTwo;
        var notEqualsByOperator = entityOne != entityTwo;
        
        // Assert
        equalsByMethod.Should().BeTrue();
        equalsByMethodAsObj.Should().BeTrue();
        equalsByHashCode.Should().BeTrue();
        equalsByOperator.Should().BeTrue();
        notEqualsByOperator.Should().BeFalse();
    }
    
    [Fact]
    public void Entity_ShouldNot_BeEqual_WhenTypesAreDifferent()
    {
        // Arrange
        var id = Guid.NewGuid();

        var entityOne = new DummyEntityOne(id, "Prop", 0);
        var entityTwo = new DummyEntityTwo(id, "Prop", 0);

        var entityOneAsObj = (object)entityOne;
        var entityTwoAsObj = (object)entityTwo;
        
        // Act
        var equalsByMethod = entityOne.Equals(entityTwo);
        var equalsByMethodAsObj = entityOneAsObj.Equals(entityTwoAsObj);
        var equalsByHashCode = entityOne.GetHashCode() == entityTwo.GetHashCode();
        var equalsByOperator = entityOne == entityTwo;
        var notEqualsByOperator = entityOne != entityTwo;
        
        // Assert
        equalsByMethod.Should().BeFalse();
        equalsByMethodAsObj.Should().BeFalse();
        equalsByHashCode.Should().BeFalse();
        equalsByOperator.Should().BeFalse();
        notEqualsByOperator.Should().BeTrue();
    }
    
    [Fact]
    public void Entity_ShouldNot_BeEqual_WhenIdsAreDifferent()
    {
        // Arrange
        var id1 = new Guid("10ED5CB7-0260-4E77-AD40-A41E1A87CFAD");
        var id2 = new Guid("8F2DA061-CB1A-4A89-B6B3-9729A1259C87");

        var entityOne = new DummyEntityOne(id1, "Prop", 0);
        var entityTwo = new DummyEntityOne(id2, "Prop", 0);

        var entityOneAsObj = (object)entityOne;
        var entityTwoAsObj = (object)entityTwo;
        
        // Act
        var equalsByMethod = entityOne.Equals(entityTwo);
        var equalsByMethodAsObj = entityOneAsObj.Equals(entityTwoAsObj);
        var equalsByHashCode = entityOne.GetHashCode() == entityTwo.GetHashCode();
        var equalsByOperator = entityOne == entityTwo;
        var notEqualsByOperator = entityOne != entityTwo;
        
        // Assert
        equalsByMethod.Should().BeFalse();
        equalsByMethodAsObj.Should().BeFalse();
        equalsByHashCode.Should().BeFalse();
        equalsByOperator.Should().BeFalse();
        notEqualsByOperator.Should().BeTrue();
    }
    
    [Fact]
    public void ValueObject_ShouldNot_BeEqual_WhenOtherIsNull()
    {
        // Arrange
        var entityOne = new DummyEntityOne(new Guid("D050624C-F7C8-44C8-A4D0-ED5F3CEED066"), "Prop", 0);
        var entityOneAsObject = (object)entityOne;
        
        // Act
        var equalsByMethod = entityOne.Equals(null);
        var equalsByMethodAsObj = entityOneAsObject.Equals(null);
        var equalsByOperator = entityOne == null;
        var notEqualsByOperator = entityOne != null;
        
        // Assert
        equalsByMethod.Should().BeFalse();
        equalsByMethodAsObj.Should().BeFalse();
        equalsByOperator.Should().BeFalse();
        notEqualsByOperator.Should().BeTrue();
    }

    [Fact]
    public void ValueObject_ShouldNot_BeEqual_WhenBothAreNull()
    {
        // Arrange
        DummyEntityOne? one = null;
        DummyEntityOne? two = null;
        
        // Act
        var equalsByOperator = one == two;
        var notEqualsByOperator = one != two;
        
        // Assert
        equalsByOperator.Should().BeFalse();
        notEqualsByOperator.Should().BeTrue();
    }
    
    private class DummyEntityOne : Entity
    {
        public string StringProp { get; }
        public int IntProp { get; }

        public DummyEntityOne(Guid id, string stringProp, int intProp) : base(id)
        {
            StringProp = stringProp;
            IntProp = intProp;
        }
    }
    
    private class DummyEntityTwo : Entity
    {
        public string StringProp { get; }
        public int IntProp { get; }

        public DummyEntityTwo(Guid id, string stringProp, int intProp) : base(id)
        {
            StringProp = stringProp;
            IntProp = intProp;
        }
    }
}