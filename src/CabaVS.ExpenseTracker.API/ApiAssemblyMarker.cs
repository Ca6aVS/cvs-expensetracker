using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CabaVS.ExpenseTracker.API;

[ExcludeFromCodeCoverage]
public static class ApiAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(ApiAssemblyMarker).Assembly;
}