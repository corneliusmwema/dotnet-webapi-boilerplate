using System.Security.Claims;

namespace FSH.Framework.Application.Common;

public interface ICurrentUser
{
    string? Name { get; }

    DefaultIdType GetUserId();

    string? GetUserEmail();

    string? GetTenant();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();
}