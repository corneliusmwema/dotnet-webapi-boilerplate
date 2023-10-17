using Ardalis.Specification;
using FSH.WebApi.Catalog.Domain.Products;

namespace FSH.WebApi.Catalog.Application.Products.Specifications;

public class ProductsByBrandSpec : Specification<Product>
{
    public ProductsByBrandSpec(DefaultIdType brandId) =>
        Query.Where(p => p.BrandId == brandId);
}
