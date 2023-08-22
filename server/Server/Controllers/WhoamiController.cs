using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IiroKi.Server.Controllers;

[ApiController]
[Route("whoami")]
[Authorize]
public class WhoamiController : UserControllerBase
{
    private readonly ILogger<WhoamiController> _logger;

    public WhoamiController(ILogger<WhoamiController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult WhoAmI()
    {
        var userId = GetUserId();
        var organizationId = GetOrganizationId();
        _logger.LogDebug("User ID: {userId}, Organization ID: {organizationId}", userId, organizationId);
        return Ok(new { UserId = userId, OrganizationId = organizationId });
    }
}
