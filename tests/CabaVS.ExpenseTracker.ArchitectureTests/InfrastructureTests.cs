using CabaVS.ExpenseTracker.API;
using CabaVS.ExpenseTracker.Infrastructure;
using CabaVS.ExpenseTracker.Presentation;
using FluentAssertions;
using NetArchTest.Rules;

namespace CabaVS.ExpenseTracker.ArchitectureTests;

public class InfrastructureTests
{
    [Fact]
    public void Infrastructure_ShouldNot_HaveDependenciesOtherThanApplication()
    {
        // Arrange & Act
        var result = Types.InAssembly(InfrastructureAssemblyMarker.Assembly)
            .ShouldNot().HaveDependencyOnAny(
                typeof(PresentationAssemblyMarker).Namespace, 
                typeof(ApiAssemblyMarker).Namespace)
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}