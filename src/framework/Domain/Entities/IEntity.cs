using FSH.Framework.Domain.Events;

namespace FSH.Framework.Domain.Entities;

public interface IEntity
{
    HashSet<DomainEvent> DomainEvents { get; }
}

public interface IEntity<out TId> : IEntity
{
    TId Id { get; }
}