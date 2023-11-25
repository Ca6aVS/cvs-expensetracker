using System.Reflection;

namespace CabaVS.ExpenseTracker.Infrastructure;

public static class InfrastructureAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(InfrastructureAssemblyMarker).Assembly;
}