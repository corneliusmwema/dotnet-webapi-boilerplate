using Carter;
using FSH.Framework.Infrastructure.Endpoints;
using FSH.WebApi.Catalog.Application.Products.Features;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.WebApi.Catalog.Infrastructure;
public class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapModuleEndpoints("catalog", "products")
        .MapGet("", CreateProduct);
    }

    internal async Task<IResult> CreateProduct(ISender sender, CreateProduct.Request request)
    {
        return Results.Ok(await sender.Send(request));
    }
}
