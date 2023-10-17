using FSH.Framework.Application.Persistence;
using FSH.WebApi.Catalog.Application.Products.Dtos;
using FSH.WebApi.Catalog.Application.Products.Exceptions;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Catalog.Application.Products.Features;
internal class GetProductByIdDapper
{
    public class Request : IRequest<ProductDto>
    {
        public Guid Id { get; set; }

        public Request(Guid id) => Id = id;
    }

    public class Handler : IRequestHandler<Request, ProductDto>
    {
        private readonly IDapperRepository _repository;
        private readonly IStringLocalizer _t;

        public Handler(IDapperRepository repository, IStringLocalizer<Handler> localizer) =>
            (_repository, _t) = (repository, localizer);

        public async Task<ProductDto> Handle(Request request, CancellationToken cancellationToken)
        {
            var product = await _repository.QueryFirstOrDefaultAsync<Product>(
                $"SELECT * FROM Catalog.\"Products\" WHERE \"Id\"  = '{request.Id}' AND \"TenantId\" = '@tenant'",
                cancellationToken: cancellationToken);
            _ = product ?? throw new ProductNotFoundException(request.Id);
            return new ProductDto
            {
                Id = product.Id,
                BrandId = product.BrandId,
                BrandName = string.Empty,
                Description = product.Description,
                ImagePath = product.ImagePath,
                Name = product.Name,
                Rate = product.Rate
            };
        }
    }
}
