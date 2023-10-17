using Ardalis.Specification;
using FSH.WebApi.Catalog.Domain.Products;

namespace FSH.WebApi.Catalog.Application.Products.Specifications;

public class ProductByNameSpec : Specification<Product>, ISingleResultSpecification<Product>
{
    public ProductByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}