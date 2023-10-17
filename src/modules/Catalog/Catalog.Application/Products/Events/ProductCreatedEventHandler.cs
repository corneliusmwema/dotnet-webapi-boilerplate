using FSH.Framework.Application.Events;
using FSH.Framework.Domain.Events;
using FSH.WebApi.Catalog.Domain.Products;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Catalog.Application.Products.Events;

public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    : EventNotificationHandler<EntityCreatedEvent<Product>>
{
    public override Task Handle(EntityCreatedEvent<Product> @event, CancellationToken cancellationToken)
    {
        logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}