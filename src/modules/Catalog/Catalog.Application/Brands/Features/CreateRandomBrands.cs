using FSH.Framework.Application.Jobs;
using FSH.WebApi.Catalog.Application.Brands.Jobs;
using MediatR;

namespace FSH.WebApi.Catalog.Application.Brands.Features;
public static class CreateRandomBrands
{
    public sealed class Request : IRequest<string>
    {
        public int NSeed { get; set; }
    }

    internal sealed class Handler(IJobService jobService) : IRequestHandler<Request, string>
    {
        public Task<string> Handle(Request request, CancellationToken cancellationToken)
        {
            string jobId = jobService.Enqueue<IBrandGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
            return Task.FromResult(jobId);
        }
    }
}
