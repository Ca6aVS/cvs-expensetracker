using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CabaVS.ExpenseTracker.Presentation;

[ExcludeFromCodeCoverage]
public static class PresentationAssemblyMarker
{
    public static readonly Assembly Assembly = typeof(PresentationAssemblyMarker).Assembly;
}