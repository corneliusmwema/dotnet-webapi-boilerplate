using Ardalis.Specification;
using FSH.Framework.Application.Persistence;
using FSH.WebApi.Catalog.Application.Products.Dtos;
using FSH.WebApi.Catalog.Application.Products.Exceptions;
using FSH.WebApi.Catalog.Application.Products.Specifications;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;

namespace FSH.WebApi.Catalog.Application.Products.Features;
internal static class GetProductById
{
    internal sealed class Request : IRequest<ProductDetailsDto>
    {
        public Guid Id { get; set; }
        public Request(Guid id) => Id = id;
    }

    public class Handler : IRequestHandler<Request, ProductDetailsDto>
    {
        private readonly IRepository<Product> _repository;
        public Handler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductDetailsDto> Handle(Request request, CancellationToken cancellationToken) =>
            await _repository.FirstOrDefaultAsync(
                (ISpecification<Product, ProductDetailsDto>)new ProductByIdWithBrandSpec(request.Id), cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);
    }
}
