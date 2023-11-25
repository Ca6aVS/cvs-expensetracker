using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CabaVS.ExpenseTracker.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(ApplicationAssemblyMarker).Assembly;
}