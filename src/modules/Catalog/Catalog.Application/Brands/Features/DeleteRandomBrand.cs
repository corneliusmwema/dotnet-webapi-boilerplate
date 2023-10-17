using FSH.Framework.Application.Jobs;
using FSH.WebApi.Catalog.Application.Brands.Jobs;
using MediatR;

namespace FSH.WebApi.Catalog.Application.Brands.Features;
public static class DeleteRandomBrand
{
    public sealed class Request : IRequest<string>
    {
    }

    internal sealed class Handler(IJobService jobService) : IRequestHandler<Request, string>
    {
        public Task<string> Handle(Request request, CancellationToken cancellationToken)
        {
            string jobId = jobService.Schedule<IBrandGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
            return Task.FromResult(jobId);
        }
    }
}
