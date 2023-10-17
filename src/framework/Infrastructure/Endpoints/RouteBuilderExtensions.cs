using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Framework.Infrastructure.Endpoints;
public static class RouteBuilderExtensions
{
    public static RouteGroupBuilder MapModuleEndpoints(this IEndpointRouteBuilder app, string module, string subModule = "")
    {
        string route = $"/api/{module}";
        if (string.IsNullOrEmpty(subModule)) route = $"/{subModule}";
        return app
            .MapGroup(route)
            .WithGroupName(module)
            .WithTags(module, subModule)
            .WithOpenApi();
    }
}
