using FSH.Framework.Application.Exceptions;

namespace FSH.WebApi.Catalog.Application.Brands.Exceptions;
internal class BrandNotFoundException : NotFoundException
{
    public BrandNotFoundException(Guid brandId) : base($"Brand with ID '{brandId}' is not found.")
    {
    }
}

