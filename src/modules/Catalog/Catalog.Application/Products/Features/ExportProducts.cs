using FSH.Framework.Application.Exporters;
using FSH.Framework.Application.Paging;
using FSH.Framework.Application.Persistence;
using FSH.WebApi.Catalog.Application.Products.Specifications;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;

namespace FSH.WebApi.Catalog.Application.Products.Features;
internal class ExportProducts
{
    public class Request : BaseFilter, IRequest<Stream>
    {
        public Guid? BrandId { get; set; }
        public decimal? MinimumRate { get; set; }
        public decimal? MaximumRate { get; set; }
    }

    public class Handler : IRequestHandler<Request, Stream>
    {
        private readonly IReadRepository<Product> _repository;
        private readonly IExcelWriter _excelWriter;

        public Handler(IReadRepository<Product> repository, IExcelWriter excelWriter)
        {
            _repository = repository;
            _excelWriter = excelWriter;
        }

        public async Task<Stream> Handle(Request request, CancellationToken cancellationToken)
        {
            var spec = new ExportProductsWithBrandsSpecification(request);
            var list = await _repository.ListAsync(spec, cancellationToken);
            return _excelWriter.WriteToStream(list);
        }
    }
}
