using FSH.Framework.Application.Persistence;
using FSH.Framework.Domain.Events;
using FSH.WebApi.Catalog.Application.Products.Exceptions;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Catalog.Application.Products.Features;
public static class DeleteProduct
{
    internal sealed class Request : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Request(Guid id) => Id = id;
    }

    internal sealed class Handler : IRequestHandler<Request, Guid>
    {
        private readonly IRepository<Product> _repository;
        private readonly IStringLocalizer _t;
        public Handler(IRepository<Product> repository, IStringLocalizer<Handler> localizer) =>
            (_repository, _t) = (repository, localizer);

        public async Task<Guid> Handle(Request request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            _ = product ?? throw new ProductNotFoundException(request.Id);
            product.DomainEvents.Add(EntityDeletedEvent.WithEntity(product));
            await _repository.DeleteAsync(product, cancellationToken);
            return request.Id;
        }
    }
}
