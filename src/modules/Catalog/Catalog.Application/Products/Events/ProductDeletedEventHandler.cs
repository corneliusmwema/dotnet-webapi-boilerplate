using FSH.Framework.Application.Events;
using FSH.Framework.Domain.Events;
using FSH.WebApi.Catalog.Domain.Products;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Catalog.Application.Products.Events;

public class ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger)
    : EventNotificationHandler<EntityDeletedEvent<Product>>
{
    public override Task Handle(EntityDeletedEvent<Product> @event, CancellationToken cancellationToken)
    {
        logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}