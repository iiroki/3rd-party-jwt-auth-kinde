using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IiroKi.Server.Controllers;

[ApiController]
[Route("whoami")]
[Authorize]
public class WhoamiController : ControllerBase
{
    [HttpGet]
    public IActionResult WhoAmI()
    {
        var userId = GetUserId();
        var organizationId = GetOrganizationId();
        var roles = GetUserRoles();
        return Ok(new { UserId = userId, OrganizationId = organizationId, UserRoles = roles });
    }

    protected string GetUserId()
    {
        var userId = User.Identity?.Name;
        if (userId != null)
        {
            return userId;
        }

        throw new InvalidOperationException("User ID not found");
    }

    protected string GetOrganizationId()
    {
        var organizationIdClaim = User.Claims.FirstOrDefault(c => c.Type == "org_code");
        if (organizationIdClaim != null)
        {
            return organizationIdClaim.Value;
        }

        throw new InvalidOperationException("Organization ID claim not found");
    }

    protected IEnumerable<string> GetUserRoles()
    {
        var roles = new List<string>();
        foreach (var identity in User.Identities)
        {
            roles.AddRange(identity.Claims.Where(c => c.Type == identity.RoleClaimType).Select(c => c.Value));
        }

        return roles;
    }
}
