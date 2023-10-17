using Ardalis.Specification;
using FSH.WebApi.Catalog.Domain.Brands;

namespace FSH.WebApi.Catalog.Application.Brands.Specifications;

public class BrandByNameSpec : Specification<Brand>, ISingleResultSpecification<Brand>
{
    public BrandByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}