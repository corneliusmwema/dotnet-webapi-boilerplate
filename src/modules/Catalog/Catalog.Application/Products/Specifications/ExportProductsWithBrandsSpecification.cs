using Ardalis.Specification;
using FSH.Framework.Application.Specification;
using FSH.WebApi.Catalog.Application.Products.Dtos;
using FSH.WebApi.Catalog.Application.Products.Features;
using FSH.WebApi.Catalog.Domain.Products;

namespace FSH.WebApi.Catalog.Application.Products.Specifications;
internal class ExportProductsWithBrandsSpecification : EntitiesByBaseFilterSpec<Product, ProductExportDto>
{
    public ExportProductsWithBrandsSpecification(ExportProducts.Request request)
        : base(request) =>
        Query
            .Include(p => p.Brand)
            .Where(p => p.BrandId.Equals(request.BrandId!.Value), request.BrandId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}
