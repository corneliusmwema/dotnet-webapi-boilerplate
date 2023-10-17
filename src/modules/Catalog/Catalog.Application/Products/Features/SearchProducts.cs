using FSH.Framework.Application.Paging;
using FSH.Framework.Application.Persistence;
using FSH.WebApi.Catalog.Application.Products.Dtos;
using FSH.WebApi.Catalog.Application.Products.Specifications;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;

namespace FSH.WebApi.Catalog.Application.Products.Features;
public static class SearchProducts
{
    internal sealed class Request : PaginationFilter, IRequest<PaginationResponse<ProductDto>>
    {
        public Guid? BrandId { get; set; }
        public decimal? MinimumRate { get; set; }
        public decimal? MaximumRate { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Request, PaginationResponse<ProductDto>>
    {
        private readonly IReadRepository<Product> _repository;

        public Handler(IReadRepository<Product> repository) => _repository = repository;

        public async Task<PaginationResponse<ProductDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var spec = new ProductsBySearchRequestWithBrandsSpec(request);
            return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
        }
    }
}
