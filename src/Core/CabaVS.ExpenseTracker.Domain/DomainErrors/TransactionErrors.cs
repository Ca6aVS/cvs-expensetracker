using CabaVS.ExpenseTracker.Domain.Shared;

namespace CabaVS.ExpenseTracker.Domain.DomainErrors;

public static class TransactionErrors
{
    public static readonly Error AmountIsZero = new(
        "Transaction.AmountIsZero",
        "Transaction amount(s) should not be zero.");

    public static readonly Error CategoryTypeMismatch = new(
        "Transaction.CategoryTypeMismatch",
        "Category Type used for Transaction should correspond to this Transaction Type."); 
    
    public static readonly Error SourceAndDestinationAmountDiffer = new(
        "Transaction.SourceAndDestinationAmountDiffer",
        "Source and Destination amounts are different but Currencies are same.");
    
    public static readonly Error SourceAndDestinationAmountSameOrNotProvided = new(
        "Transaction.SourceAndDestinationAmountSameOrNotProvided",
        "Destination amount is not provided or Source and Destination amounts are same but Currencies are not.");
}