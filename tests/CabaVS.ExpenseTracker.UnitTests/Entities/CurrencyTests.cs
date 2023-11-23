using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Entities;
using CabaVS.ExpenseTracker.Domain.ValueObjects;
using FluentAssertions;

namespace CabaVS.ExpenseTracker.UnitTests.Entities;

public class CurrencyTests
{
    [Fact]
    public void Currency_Should_BeCreated_WhenAllValuesAreValid()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        var name = CurrencyName.Create("Polish zloty").Value;
        const string code = "PLN";
        const string symbol = "zl";
        
        // Act
        var result = Currency.Create(id, name, code, symbol, true);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Should().Be(name);
        result.Value.Code.Value.Should().Be(code);
        result.Value.Symbol.Value.Should().Be(symbol);
    }
    
    [Fact]
    public void Currency_ShouldNot_BeCreated_WhenNameIsNotUnique()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        var name = CurrencyName.Create("Polish zloty").Value;
        const string code = "PLN";
        const string symbol = "zl";
        
        // Act
        var result = Currency.Create(id, name, code, symbol, false);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencyErrors.NameTaken(name));
    }
    
    [Fact]
    public void Currency_ShouldNot_BeCreated_WhenCodeIsEmpty()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        var name = CurrencyName.Create("Polish zloty").Value;
        const string code = "   ";
        const string symbol = "zl";
        
        // Act
        var result = Currency.Create(id, name, code, symbol, true);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencyCodeErrors.Empty());
    }
    
    [Fact]
    public void Currency_ShouldNot_BeCreated_WhenCodeIsTooLong()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        var name = CurrencyName.Create("Polish zloty").Value;
        const string code = "Too long Code.";
        const string symbol = "zl";
        
        // Act
        var result = Currency.Create(id, name, code, symbol, true);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencyCodeErrors.TooLong(code.Length));
    }
    
    [Fact]
    public void Currency_ShouldNot_BeCreated_WhenSymbolIsEmpty()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        var name = CurrencyName.Create("Polish zloty").Value;
        const string code = "PLN";
        const string symbol = "  ";
        
        // Act
        var result = Currency.Create(id, name, code, symbol, true);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencySymbolErrors.Empty());
    }
    
    [Fact]
    public void Currency_ShouldNot_BeCreated_WhenSymbolIsTooLong()
    {
        // Arrange
        var id = new Guid("92B2394C-F757-49DC-AE45-3BB069B28EE5");
        var name = CurrencyName.Create("Polish zloty").Value;
        const string code = "PLN";
        const string symbol = "Too long Symbol.";
        
        // Act
        var result = Currency.Create(id, name, code, symbol, true);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CurrencySymbolErrors.TooLong(symbol.Length));
    }
}