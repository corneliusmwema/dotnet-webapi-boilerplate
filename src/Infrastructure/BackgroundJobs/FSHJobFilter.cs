using Finbuckle.MultiTenant;
using FSH.WebApi.Infrastructure.Common;
using FSH.WebApi.Shared.Authorization;
using FSH.WebApi.Shared.Multitenancy;
using Hangfire.Client;
using Hangfire.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.WebApi.Infrastructure.BackgroundJobs;

public class FSHJobFilter : IClientFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    private readonly IServiceProvider _services;

    public FSHJobFilter(IServiceProvider services) => _services = services;

    public void OnCreating(CreatingContext filterContext)
    {
        ArgumentNullException.ThrowIfNull(filterContext, nameof(filterContext));

        Logger.InfoFormat("Set TenantId and UserId parameters to job {0}.{1}...", filterContext.Job.Method.ReflectedType?.FullName, filterContext.Job.Method.Name);

        using var scope = _services.CreateScope();

        var httpContext = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
        _ = httpContext ?? throw new InvalidOperationException("Can't create a TenantJob without HttpContext.");

        var tenantInfo = scope.ServiceProvider.GetRequiredService<ITenantInfo>();
        filterContext.SetJobParameter(MultitenancyConstants.TenantIdName, tenantInfo);

        string? userId = httpContext.User.GetUserId();
        filterContext.SetJobParameter(QueryStringKeys.UserId, userId);
    }

    public void OnCreated(CreatedContext context) =>
        Logger.InfoFormat(
            "Job created with parameters {0}",
            context.Parameters.Select(x => x.Key + "=" + x.Value).Aggregate((s1, s2) => s1 + ";" + s2));
}