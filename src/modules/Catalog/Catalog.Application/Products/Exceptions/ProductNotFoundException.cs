using FSH.Framework.Application.Exceptions;

namespace FSH.WebApi.Catalog.Application.Products.Exceptions;
internal class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid productId) : base($"Product with ID '{productId}' is not found.")
    {
    }
}
