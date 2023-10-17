using Ardalis.Specification;
using FSH.Framework.Application.Paging;
using FSH.Framework.Application.Specification;
using FSH.WebApi.Catalog.Application.Brands.Dtos;
using FSH.WebApi.Catalog.Application.Brands.Features;
using FSH.WebApi.Catalog.Domain.Brands;

namespace FSH.WebApi.Catalog.Application.Brands.Specifications;
public class BrandsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Brand, BrandDto>
{
    public BrandsBySearchRequestSpec(SearchBrands.Request request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}
