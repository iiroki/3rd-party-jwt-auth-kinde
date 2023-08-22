using Microsoft.AspNetCore.Mvc;

namespace IiroKi.Server.Controllers;

/// <summary>
/// Base class for an API controller that requires authenticated users.
/// </summary>
public abstract class UserControllerBase : ControllerBase
{
    private static readonly string UserIdClaimName = "sub";
    private static readonly string OrganizationIdClaimName = "org_code";

    protected string GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == UserIdClaimName);
        if (userIdClaim != null)
        {
            return userIdClaim.Value;
        }

        throw new InvalidOperationException("User ID claim not found");
    }

    protected string GetOrganizationId()
    {
        var organizationIdClaim = User.Claims.FirstOrDefault(c => c.Type == OrganizationIdClaimName);
        if (organizationIdClaim != null)
        {
            return organizationIdClaim.Value;
        }

        throw new InvalidOperationException("Organization ID claim not found");
    }
}
