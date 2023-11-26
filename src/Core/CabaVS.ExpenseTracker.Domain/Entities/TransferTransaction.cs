using CabaVS.ExpenseTracker.Domain.DomainErrors;
using CabaVS.ExpenseTracker.Domain.Primitives;
using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.Entities;

public sealed class TransferTransaction : Entity
{
    public DateOnly Date { get; set; }
    public HashSet<string> Tags { get; }

    public Balance Source { get; }
    public decimal SourceAmount { get; private set; }
    
    public Balance Destination { get; }
    public decimal DestinationAmount { get; private set; }
    
    private TransferTransaction(Guid id,
        DateOnly date,
        IEnumerable<string> tags,
        Balance source,
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

    public static Result<TransferTransaction> Create(
        Guid id,
        DateOnly date,
        IEnumerable<string> tags,
        Balance source,
        decimal sourceAmount,
        Balance destination,
        decimal? destinationAmount = null)
    {
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
        destination.Amount += destinationAmountToUse;

        return new TransferTransaction(id, date, tags, source, sourceAmount, destination, destinationAmountToUse);
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

        Destination.Amount -= DestinationAmount;
        Destination.Amount += destinationAmountToUse;
        
        SourceAmount = source;
        DestinationAmount = destinationAmountToUse;
        
        return Result.Success();
    }
}