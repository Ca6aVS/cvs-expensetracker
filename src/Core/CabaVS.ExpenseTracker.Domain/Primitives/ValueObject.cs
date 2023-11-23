namespace CabaVS.ExpenseTracker.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetAtomicValues();
    
    public bool Equals(ValueObject? other)
    {
        return other is not null && other.GetType() == GetType() && GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject valueObject && Equals(valueObject);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            GetType()
                .GetHashCode(),
            GetAtomicValues()
                .Aggregate(
                    default(int),
                    HashCode.Combine)) * 47;
    }
    
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left is not null && left.Equals(right);
    }
    
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}