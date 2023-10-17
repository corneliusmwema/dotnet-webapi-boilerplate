using FSH.Framework.Domain.Events;
using MediatR;

namespace FSH.Framework.Application.Events;

// This is just a shorthand to make it a bit easier to create event handlers for specific events.
public interface IEventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
    where TEvent : IEvent
{
}

public abstract class EventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
    where TEvent : IEvent
{
    public Task Handle(EventNotification<TEvent> notification, CancellationToken cancellationToken) =>
        Handle(notification.Event, cancellationToken);

    public abstract Task Handle(TEvent fshEvent, CancellationToken cancellationToken);
}