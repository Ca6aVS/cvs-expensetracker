using System.Reflection;

namespace CabaVS.ExpenseTracker.Domain;

public static class DomainAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(DomainAssemblyMarker).Assembly;
}