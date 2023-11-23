namespace CabaVS.ExpenseTracker.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public bool Equals(Entity? other)
    {
        return other is not null && other.GetType() == GetType() && other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && Equals(entity);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id.GetHashCode(), GetType().GetHashCode()) * 47;
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        return left is not null && left.Equals(right);
    }
    
    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }
}