namespace CabaVS.ExpenseTracker.Domain.Shared;

public sealed record Error(string Code, string Message)
{
    public static Error None => new(string.Empty, string.Empty);
}