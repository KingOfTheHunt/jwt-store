namespace JwtStore.Core.Contexts.SharedContext.Entities;

public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; }

    protected Entity() =>
        Id = Guid.NewGuid();

    public bool Equals(Guid id) => 
        Id == id;
}