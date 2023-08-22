using Microsoft.IdentityModel.Tokens;
using IiroKi.Server.Services;

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
                ValidateAudience = false, // TODO: true
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true, // <-- Important!
                // Values:
                ValidIssuer = kindeUrl,
                IssuerSigningKey = jwks.Keys.First()
                // TODO: More rules?
            };
        }
        catch (Exception ex)
        {
            throw new InitException("Could not fetch JSON Web Key Set from Kinde", ex);
        }
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
