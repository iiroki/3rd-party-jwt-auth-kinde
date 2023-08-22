using Microsoft.IdentityModel.Tokens;

namespace IiroKi.Server.Services;

/// <summary>
/// Kinde authentication service.
/// </summary>
public class KindeService
{
    public static async Task<JsonWebKeySet> FetchJwksAsync(string kindeUrl)
    {
        using HttpClient client = new();
        var res = await client.GetAsync($"{kindeUrl}/.well-known/jwks");
        var jsonStr = await res.Content.ReadAsStringAsync();
        return new JsonWebKeySet(jsonStr);
    }
}
