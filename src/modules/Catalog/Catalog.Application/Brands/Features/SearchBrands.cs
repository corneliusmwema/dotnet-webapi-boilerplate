using FSH.Framework.Application.Paging;
using FSH.Framework.Application.Persistence;
using FSH.WebApi.Catalog.Application.Brands.Dtos;
using FSH.WebApi.Catalog.Application.Brands.Specifications;
using FSH.WebApi.Catalog.Domain.Brands;
using MediatR;

namespace FSH.WebApi.Catalog.Application.Brands.Features;
public static class SearchBrands
{
    public class Request : PaginationFilter, IRequest<PaginationResponse<BrandDto>>
    {
    }
    public class SearchBrandsRequestHandler : IRequestHandler<Request, PaginationResponse<BrandDto>>
    {
        private readonly IReadRepository<Brand> _repository;

        public SearchBrandsRequestHandler(IReadRepository<Brand> repository) => _repository = repository;

        public async Task<PaginationResponse<BrandDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            var spec = new BrandsBySearchRequestSpec(request);
            return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
