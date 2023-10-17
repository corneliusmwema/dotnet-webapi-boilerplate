using Ardalis.Specification;
using FSH.Framework.Application.Persistence;
using FSH.WebApi.Catalog.Application.Brands.Dtos;
using FSH.WebApi.Catalog.Application.Brands.Exceptions;
using FSH.WebApi.Catalog.Application.Brands.Specifications;
using FSH.WebApi.Catalog.Domain.Brands;
using MediatR;

namespace FSH.WebApi.Catalog.Application.Brands.Features;
public static class GetBrandById
{
    public sealed class Request : IRequest<BrandDto>
    {
        public Guid Id { get; set; }

        public Request(Guid id) => Id = id;
    }
    internal sealed class Handler(IRepository<Brand> repository) : IRequestHandler<Request, BrandDto>
    {
        public async Task<BrandDto> Handle(Request request, CancellationToken cancellationToken) =>
            await repository.FirstOrDefaultAsync(
                (ISpecification<Brand, BrandDto>)new BrandByIdSpec(request.Id), cancellationToken)
            ?? throw new BrandNotFoundException(request.Id);
    }
}
