using CabaVS.ExpenseTracker.Domain.Shared;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.Shared;

public class ResultTests
{
    [Fact]
    public void Result_Success_Should_MakeSuccessfulResult()
    {
        // Act
        var result = Result.Success();
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().Be(Error.None);
    }
    
    [Fact]
    public void Result_Failure_Should_MakeFailedResultWithError()
    {
        // Arrange
        var error = new Error("Error Code", "Error Message");
        
        // Act
        var result = Result.Failure(error);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
    
    [Fact]
    public void Result_ImplicitOperatorOnError_Should_MakeFailedResultWithError()
    {
        // Arrange
        var error = new Error("Error Code", "Error Message");
        
        // Act
        Result result = error;
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }

    [Fact]
    public void Result_Should_ThrowException_WhenSuccessAndErrorProvided()
    {
        // Arrange
        const bool isSuccess = true;
        var error = new Error("Error Code", "Error Message");
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = new DummyResult(isSuccess, error));
    }
    
    [Fact]
    public void Result_Should_ThrowException_WhenFailureAndErrorIsNone()
    {
        // Arrange
        const bool isSuccess = false;
        var error = Error.None;
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _ = new DummyResult(isSuccess, error));
    }
    
    [Fact]
    public void ResultGeneric_ImplicitOperatorOnValue_Should_MakeSuccessfulResultWithValue()
    {
        // Arrange
        const string value = "Result Value";
        
        // Act
        Result<string> result = value; 
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().Be(Error.None);
        
        result.Value.Should().Be(value);
    }
    
    [Fact]
    public void ResultGeneric_Failure_Should_MakeFailedResultWithError()
    {
        // Arrange
        var error = new Error("Error Code", "Error Message");
        
        // Act
        var result = Result<string>.Failure(error);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
        
        result.Invoking(r => r.Value)
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Cannot access value on a failed Result.");
    }
    
    [Fact]
    public void ResultGeneric_ImplicitOperatorOnError_Should_MakeFailedResultWithError()
    {
        // Arrange
        var error = new Error("Error Code", "Error Message");
        
        // Act
        Result<string> result = error;
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
        
        result.Invoking(r => r.Value)
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Cannot access value on a failed Result.");
    }
    
    private class DummyResult(bool isSuccess, Error error) : Result(isSuccess, error);
}