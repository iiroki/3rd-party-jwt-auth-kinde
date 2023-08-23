using System.IdentityModel.Tokens.Jwt;
using IiroKi.Server.Defaults;
using IiroKi.Server.Services;
using Microsoft.IdentityModel.Tokens;

namespace IiroKi.Server.Config;

/// <summary>
/// Helper methods for initializing the application.
/// </summary>
public static class Init
{
    private const string KindeUrlEnv = "KINDE_URL";

    /// <summary>
    /// Creates JWT validation parameters based on Kinde JSON Web Key (JWK).
    /// </summary>
    public static async Task<TokenValidationParameters> CreateJwtValidationParamsAsync(WebApplicationBuilder builder)
    {
        try
        {
            var kindeUrl = builder.Configuration[KindeUrlEnv];
            if (string.IsNullOrEmpty(kindeUrl))
            {
                throw new InvalidOperationException("Kinde URL is null or empty");
            }

            var jwks = await KindeService.FetchJwksAsync(kindeUrl);
            return new TokenValidationParameters()
            {
                // Validations:
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true, // <-- Important!
                // Values:
                ValidAudience = "http://localhost:8080", // TODO: Make this configurable.
                ValidIssuer = kindeUrl,
                IssuerSigningKey = jwks.Keys.First(),
                // Options:
                NameClaimType = KindeService.UserIdClaimType,
                RoleClaimType = ServerDefaults.UserRoleClaimType // Should be extracted from JWT!
            };
        }
        catch (Exception ex)
        {
            throw new InitException("Could not fetch JSON Web Key Set from Kinde", ex);
        }
    }

    public static void SetupJwtHandler(JwtSecurityTokenHandler jwtHandler)
    {
        jwtHandler.InboundClaimTypeMap.Clear();
        jwtHandler.InboundClaimFilter.Add(ServerDefaults.UserRoleClaimType); // <-- Remove user roles from JWT
    }

    /// <summary>
    /// An exception that happens during application initiaization
    /// and was caused by another exception.
    /// </summary>
    public class InitException : Exception
    {
        public InitException(string message, Exception innerEx) : base(message, innerEx)
        {
            // NOP
        }
    }
}
