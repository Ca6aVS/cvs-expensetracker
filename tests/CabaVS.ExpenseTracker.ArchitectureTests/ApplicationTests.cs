using CabaVS.ExpenseTracker.API;
using CabaVS.ExpenseTracker.Application;
using CabaVS.ExpenseTracker.Infrastructure;
using CabaVS.ExpenseTracker.Presentation;
using FluentAssertions;
using NetArchTest.Rules;

namespace CabaVS.ExpenseTracker.ArchitectureTests;

public class ApplicationTests
{
    [Fact]
    public void Application_ShouldNot_HaveDependenciesOtherThanDomain()
    {
        // Arrange & Act
        var result = Types.InAssembly(ApplicationAssemblyMarker.Assembly)
            .ShouldNot().HaveDependencyOnAny(
                typeof(InfrastructureAssemblyMarker).Namespace,
                typeof(PresentationAssemblyMarker).Namespace, 
                typeof(ApiAssemblyMarker).Namespace)
            .GetResult();
        
        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}