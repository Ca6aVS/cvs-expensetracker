using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CabaVS.ExpenseTracker.Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(InfrastructureAssemblyMarker).Assembly;
}