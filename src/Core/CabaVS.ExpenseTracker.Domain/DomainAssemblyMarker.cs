using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CabaVS.ExpenseTracker.Domain;

[ExcludeFromCodeCoverage]
public static class DomainAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(DomainAssemblyMarker).Assembly;
}