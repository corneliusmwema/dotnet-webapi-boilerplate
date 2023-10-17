using Ardalis.Specification;
using FSH.Framework.Application.Paging;
using FSH.Framework.Application.Specification;
using FSH.WebApi.Catalog.Application.Products.Dtos;
using FSH.WebApi.Catalog.Application.Products.Features;
using FSH.WebApi.Catalog.Domain.Products;

namespace FSH.WebApi.Catalog.Application.Products.Specifications;

internal sealed class ProductsBySearchRequestWithBrandsSpec : EntitiesByPaginationFilterSpec<Product, ProductDto>
{
    public ProductsBySearchRequestWithBrandsSpec(SearchProducts.Request request)
        : base(request) =>
        Query
            .Include(p => p.Brand)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.BrandId.Equals(request.BrandId!.Value), request.BrandId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}