using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Enums;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.Entities;

public sealed class IncomeTransaction : Entity
{
    public DateOnly Date { get; set; }
    public HashSet<string> Tags { get; }

    public Category Source { get; }
    public decimal SourceAmount { get; private set; }
    
    public Balance Destination { get; }
    public decimal DestinationAmount { get; private set; }
    
    private IncomeTransaction(Guid id,
        DateOnly date,
        IEnumerable<string> tags,
        Category source,
        decimal sourceAmount,
        Balance destination,
        decimal destinationAmount) : base(id)
    {
        Date = date;
        Tags = new HashSet<string>(tags);
        Source = source;
        SourceAmount = sourceAmount;
        Destination = destination;
        DestinationAmount = destinationAmount;
    }

    public static Result<IncomeTransaction> Create(
        Guid id,
        DateOnly date,
        IEnumerable<string> tags,
        Category source,
        decimal sourceAmount,
        Balance destination,
        decimal? destinationAmount = null)
    {
        if (source.Type != CategoryType.Income)
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
        
        destination.Amount += destinationAmountToUse;

        return new IncomeTransaction(id, date, tags, source, sourceAmount, destination, destinationAmountToUse);
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

        Destination.Amount -= DestinationAmount;
        Destination.Amount += destinationAmountToUse;
        
        SourceAmount = source;
        DestinationAmount = destinationAmountToUse;
        
        return Result.Success();
    }
}