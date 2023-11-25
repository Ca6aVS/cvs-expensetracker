using CabaVS.ExpenseTracker.Domain;
using CabaVS.ExpenseTracker.Domain.Primitives;
using FluentAssertions;
using NetArchTest.Rules;

namespace CabaVS.ExpenseTracker.ArchitectureTests;

public class DomainTests
{
    [Fact]
    public void Domain_DomainErrors_Should_BeStatic()
    {
        // Arrange & Act
        var result = Types.InAssembly(DomainAssemblyMarker.Assembly)
            .That().ResideInNamespaceStartingWith("CabaVS.ExpenseTracker.Domain.DomainErrors")
            .Should().BeStatic()
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Domain_Entities_Should_InheritEntity()
    {
        // Arrange & Act
        var result = Types.InAssembly(DomainAssemblyMarker.Assembly)
            .That().ResideInNamespaceStartingWith("CabaVS.ExpenseTracker.Domain.Entities")
            .Should().Inherit(typeof(Entity))
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Entities_Should_BeSealed()
    {
        // Arrange & Act
        var result = Types.InAssembly(DomainAssemblyMarker.Assembly)
            .That().ResideInNamespaceStartingWith("CabaVS.ExpenseTracker.Domain.Entities")
            .Should().BeSealed()
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Domain_Primitives_Should_BeAbstract()
    {
        // Arrange & Act
        var result = Types.InAssembly(DomainAssemblyMarker.Assembly)
            .That().ResideInNamespaceStartingWith("CabaVS.ExpenseTracker.Domain.Primitives")
            .Should().BeAbstract()
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Domain_Shared_Should_BeClasses()
    {
        // Arrange & Act
        var result = Types.InAssembly(DomainAssemblyMarker.Assembly)
            .That().ResideInNamespaceStartingWith("CabaVS.ExpenseTracker.Domain.Shared")
            .Should().BeClasses()
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Domain_ValueObjects_Should_InheritValueObject()
    {
        // Arrange & Act
        var result = Types.InAssembly(DomainAssemblyMarker.Assembly)
            .That().ResideInNamespaceStartingWith("CabaVS.ExpenseTracker.Domain.ValueObjects")
            .Should().Inherit(typeof(ValueObject))
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_ValueObjects_Should_BeSealed()
    {
        // Arrange & Act
        var result = Types.InAssembly(DomainAssemblyMarker.Assembly)
            .That().ResideInNamespaceStartingWith("CabaVS.ExpenseTracker.Domain.ValueObjects")
            .Should().BeSealed()
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}