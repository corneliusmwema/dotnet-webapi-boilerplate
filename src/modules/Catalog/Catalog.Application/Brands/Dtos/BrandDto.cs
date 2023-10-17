using FSH.Framework.Application.Common;

namespace FSH.WebApi.Catalog.Application.Brands.Dtos;

public class BrandDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}