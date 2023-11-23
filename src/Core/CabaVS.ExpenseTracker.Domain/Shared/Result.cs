namespace CabaVS.ExpenseTracker.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    
    public bool IsFailure => !IsSuccess;
    
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException("Error should be 'None' on successful Result.");
            case false when error == Error.None:
                throw new InvalidOperationException("Error should not be 'None' on failed Result.");
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }
    
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    
    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access value on a failed Result.");

    private Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }
    
    private static Result<T> Success(T value) => new(value, true, Error.None);
    public new static Result<T> Failure(Error error) => new(default, false, error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}