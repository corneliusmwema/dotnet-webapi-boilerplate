using FSH.Framework.Application.Common;
using FSH.Framework.Domain.Events;

namespace FSH.Framework.Application.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent fshEvent);
}