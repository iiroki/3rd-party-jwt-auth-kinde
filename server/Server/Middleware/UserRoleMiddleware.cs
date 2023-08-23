using System.Security.Claims;
using IiroKi.Server.Defaults;

namespace IiroKi.Server.Middleware;

/// <summary>
/// A middleware that adds role claims to the request user.
/// Default user role is added to every user for demo purposes.
/// </summary>
public class UserRoleMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
    {
        var userId = ctx.User.Identity?.Name;
        if (userId != null)
        {
            // Fetch user roles based on the user ID...

            var claims = new List<Claim> { new Claim(ServerDefaults.UserRoleClaimType, "default") };

            var roleClaimIdentity = new ClaimsIdentity(
                claims,
                null, // No authentication
                ServerDefaults.UndefinedClaimType,
                ServerDefaults.UserRoleClaimType
            );
        
            ctx.User.AddIdentity(roleClaimIdentity);
        }

        await next(ctx);
    }
}
