using CabaVS.ExpenseTracker.Domain.Shared;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.Shared;

public class ErrorTests
{
    [Fact]
    public void Error_Should_BeEqual_WhenAllPropertiesAreSame()
    {
        // Arrange
        const string code = "Error Code";
        const string message = "Error Message";

        var error1 = new Error(code, message);
        var error2 = new Error(code, message);
        
        // Act
        var equalByOperator = error1 == error2;
        var equalByMethod = error1.Equals(error2);
        var equalByHashcode = error1.GetHashCode() == error2.GetHashCode();
        
        // Assert
        equalByOperator.Should().BeTrue();
        equalByMethod.Should().BeTrue();
        equalByHashcode.Should().BeTrue();
    }
    
    [Fact]
    public void Error_ShouldNot_BeEqual_WhenCodeDiffers()
    {
        // Arrange
        const string code1 = "Error Code 1";
        const string code2 = "Error Code 2";
        const string message = "Error Message";

        var error1 = new Error(code1, message);
        var error2 = new Error(code2, message);
        
        // Act
        var equalByOperator = error1 == error2;
        var equalByMethod = error1.Equals(error2);
        var equalByHashcode = error1.GetHashCode() == error2.GetHashCode();
        
        // Assert
        equalByOperator.Should().BeFalse();
        equalByMethod.Should().BeFalse();
        equalByHashcode.Should().BeFalse();
    }
    
    [Fact]
    public void Error_ShouldNot_BeEqual_WhenMessageDiffers()
    {
        // Arrange
        const string code = "Error Code";
        const string message1 = "Error Message 1";
        const string message2 = "Error Message 2";

        var error1 = new Error(code, message1);
        var error2 = new Error(code, message2);
        
        // Act
        var equalByOperator = error1 == error2;
        var equalByMethod = error1.Equals(error2);
        var equalByHashcode = error1.GetHashCode() == error2.GetHashCode();
        
        // Assert
        equalByOperator.Should().BeFalse();
        equalByMethod.Should().BeFalse();
        equalByHashcode.Should().BeFalse();
    }
}