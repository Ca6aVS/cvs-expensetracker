using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Enums;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.Entities;

public sealed class ExpenseTransaction : Entity
{
    public DateOnly Date { get; set; }
    public HashSet<string> Tags { get; }

    public Balance Source { get; }
    public decimal SourceAmount { get; private set; }
    
    public Category Destination { get; }
    public decimal DestinationAmount { get; private set; }
    
    private ExpenseTransaction(Guid id,
        DateOnly date,
        IEnumerable<string> tags,
        Balance source,
        decimal sourceAmount,
        Category destination,
        decimal destinationAmount) : base(id)
    {
        Date = date;
        Tags = new HashSet<string>(tags);
        Source = source;
        SourceAmount = sourceAmount;
        Destination = destination;
        DestinationAmount = destinationAmount;
    }

    public static Result<ExpenseTransaction> Create(
        Guid id,
        DateOnly date,
        IEnumerable<string> tags,
        Balance source,
        decimal sourceAmount,
        Category destination,
        decimal? destinationAmount = null)
    {
        if (destination.Type != CategoryType.Expense)
        {
            return TransactionErrors.CategoryTypeMismatch;
        }
        
        if (destinationAmount.HasValue && source.Currency == destination.Currency && sourceAmount != destinationAmount)
        {
            return TransactionErrors.SourceAndDestinationAmountDiffer;
        }

        if ((!destinationAmount.HasValue || sourceAmount == destinationAmount) && source.Currency != destination.Currency)
        {
            return TransactionErrors.SourceAndDestinationAmountSameOrNotProvided;
        }

        var destinationAmountToUse = destinationAmount ?? sourceAmount;
        if (sourceAmount == 0 || destinationAmountToUse == 0)
        {
            return TransactionErrors.AmountIsZero;
        }

        source.Amount -= sourceAmount;

        return new ExpenseTransaction(id, date, tags, source, sourceAmount, destination, destinationAmountToUse);
    }

    public Result ChangeAmount(decimal source, decimal? destination)
    {
        if (SourceAmount == DestinationAmount && source != destination)
        {
            return TransactionErrors.SourceAndDestinationAmountDiffer;
        }

        if (SourceAmount != DestinationAmount && source == destination)
        {
            return TransactionErrors.SourceAndDestinationAmountSameOrNotProvided;
        }
        
        var destinationAmountToUse = destination ?? source;
        if (source == 0 || destinationAmountToUse == 0)
        {
            return TransactionErrors.AmountIsZero;
        }

        Source.Amount += SourceAmount;
        Source.Amount -= source;
        
        SourceAmount = source;
        DestinationAmount = destinationAmountToUse;
        
        return Result.Success();
    }
}