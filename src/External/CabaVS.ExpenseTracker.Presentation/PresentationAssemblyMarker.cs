using System.Reflection;

namespace CabaVS.ExpenseTracker.Presentation;

public static class PresentationAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(PresentationAssemblyMarker).Assembly;
}