using FSH.Framework.Application.Common;

namespace FSH.Framework.Application.Caching;

public interface ICacheKeyService : IScopedService
{
    public string GetCacheKey(string name, object id, bool includeTenantId = true);
}