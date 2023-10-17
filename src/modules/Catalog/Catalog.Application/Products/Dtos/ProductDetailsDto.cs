using FSH.Framework.Application.Common;
using FSH.WebApi.Catalog.Application.Brands.Dtos;

namespace FSH.WebApi.Catalog.Application.Products.Dtos;

public class ProductDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public string? ImagePath { get; set; }
    public BrandDto Brand { get; set; } = default!;
}