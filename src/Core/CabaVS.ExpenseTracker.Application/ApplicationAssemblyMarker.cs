using System.Reflection;

namespace CabaVS.ExpenseTracker.Application;

public static class ApplicationAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(ApplicationAssemblyMarker).Assembly;
}