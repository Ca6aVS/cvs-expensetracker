using CabaVS.ExpenseTracker.API;
using CabaVS.ExpenseTracker.Infrastructure;
using CabaVS.ExpenseTracker.Presentation;
using FluentAssertions;
using NetArchTest.Rules;

namespace CabaVS.ExpenseTracker.ArchitectureTests;

public class PresentationTests
{
    [Fact]
    public void Presentation_ShouldNot_HaveDependenciesOtherThanApplication()
    {
        // Arrange & Act
        var result = Types.InAssembly(PresentationAssemblyMarker.Assembly)
            .ShouldNot().HaveDependencyOnAny(
                typeof(InfrastructureAssemblyMarker).Namespace, 
                typeof(ApiAssemblyMarker).Namespace)
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}