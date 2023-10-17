using FSH.Framework.Application.Exceptions;
using FSH.Framework.Application.Persistence;
using FSH.WebApi.Catalog.Application.Brands.Exceptions;
using FSH.WebApi.Catalog.Application.Products.Specifications;
using FSH.WebApi.Catalog.Domain.Brands;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Catalog.Application.Brands.Features;
public static class DeleteBrand
{
    public sealed class Request : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public Request(Guid id) => Id = id;
    }

    public class Handler : IRequestHandler<Request, Guid>
    {
        private readonly IRepositoryWithEvents<Brand> _brandRepo;
        private readonly IReadRepository<Product> _productRepo;
        private readonly IStringLocalizer _t;

        public Handler(IRepositoryWithEvents<Brand> brandRepo, IReadRepository<Product> productRepo, IStringLocalizer<Handler> localizer) =>
            (_brandRepo, _productRepo, _t) = (brandRepo, productRepo, localizer);

        public async Task<Guid> Handle(Request request, CancellationToken cancellationToken)
        {
            if (await _productRepo.AnyAsync(new ProductsByBrandSpec(request.Id), cancellationToken))
                throw new ConflictException(_t["Brand cannot be deleted as it's being used."]);
            var brand = await _brandRepo.GetByIdAsync(request.Id, cancellationToken);
            _ = brand ?? throw new BrandNotFoundException(request.Id);
            await _brandRepo.DeleteAsync(brand, cancellationToken);
            return request.Id;
        }
    }
}
