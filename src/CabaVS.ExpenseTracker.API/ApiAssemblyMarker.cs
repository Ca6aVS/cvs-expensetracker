using System.Reflection;

namespace CabaVS.ExpenseTracker.API;

public static class ApiAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(ApiAssemblyMarker).Assembly;
}