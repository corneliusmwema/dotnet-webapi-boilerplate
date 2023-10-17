using FSH.Framework.Application.Common;

namespace FSH.Framework.Application.Auditing;

public interface IAuditService : ITransientService
{
    Task<List<AuditDto>> GetUserTrailsAsync(DefaultIdType userId);
}