using FSH.Framework.Domain.Events;
using MediatR;

namespace FSH.Framework.Application.Events;

public record EventNotification<TEvent>(TEvent Event) : INotification where TEvent : IEvent;
