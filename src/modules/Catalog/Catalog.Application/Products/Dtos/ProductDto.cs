using FSH.Framework.Application.Common;

namespace FSH.WebApi.Catalog.Application.Products.Dtos;

public class ProductDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public string? ImagePath { get; set; }
    public DefaultIdType BrandId { get; set; }
    public string BrandName { get; set; } = default!;
}