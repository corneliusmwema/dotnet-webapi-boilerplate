using Ardalis.Specification;
using FSH.WebApi.Catalog.Application.Brands.Dtos;
using FSH.WebApi.Catalog.Domain.Brands;

namespace FSH.WebApi.Catalog.Application.Brands.Specifications;
public class BrandByIdSpec : Specification<Brand, BrandDto>, ISingleResultSpecification<Brand, BrandDto>
{
    public BrandByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}