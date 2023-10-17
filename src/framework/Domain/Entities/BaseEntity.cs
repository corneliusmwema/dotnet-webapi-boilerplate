using FSH.Framework.Domain.Events;
using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.Framework.Domain.Entities;

public abstract class BaseEntity : BaseEntity<DefaultIdType>
{
    protected BaseEntity() => Id = NewId.Next().ToGuid();
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; } = default!;

    [NotMapped]
    public HashSet<DomainEvent> DomainEvents { get; } = new();
}