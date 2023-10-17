using Ardalis.Specification;
using FSH.WebApi.Catalog.Application.Products.Dtos;
using FSH.WebApi.Catalog.Domain.Products;

namespace FSH.WebApi.Catalog.Application.Products.Specifications;

public class ProductByIdWithBrandSpec : Specification<Product, ProductDetailsDto>, ISingleResultSpecification<Product, ProductDetailsDto>
{
    public ProductByIdWithBrandSpec(DefaultIdType id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Brand);
}