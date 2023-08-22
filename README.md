# 3rd Party JWT Auth with Kinde

A simple demonstration about how a 3rd party user authentication service, [Kinde](https://kinde.com) in this case,
can be used to authorize API requests between a React client and an ASP.NET Core web server with JSON Web Token (JWT).

First, the client must perform authentication with Kinde in order to gain an access token (JWT).
The access token can be then be passed as Bearer Token in API requests to the server,
which verifies the access token using a JSON Web Key (JWK) from Kinde.

In total, the authentication/authorization consists of four parts:
1. The server requests a JSON Web Key Set (JWKS) from Kinde in order to verify JWTs.
2. The client performs authentication with Kinde in order to gain access token (JWT).
3. The client makes an API request to the server, passing the access token as Bearer Token.
4. The server uses a JWK from the JWKS to verify the JWT passed as Bearer Token.

**Auth flow diagram:**

![Auth flow](/images/auth-flow.drawio.png)

## Quickstart

**Both, the server and the client, must be started separately!**

### Kinde

Instructions on how to setup Kinde can be found from [Kinde Docs](https://kinde.com/docs).

**Required Kinde resources:**
- Kinde account and business
- At least one application ("SPA or Mobile Application")
- At least one user.

### Server

1. Create `appsettings.Development.json` file under `server/Server/` (if it doesn't exists)
and add your Kinde URL to it:
    ```json
    {
        "KINDE_URL": "https://your-business.kinde.com"
    }
    ```

2. Start the server in development mode (in order to read Kinde URL from `appsettings.Development.json`):
    ```bash
    ASPNETCORE_ENVIRONMENT=Development dotnet run
    ```

### Client

1. Create `.env` file under `client/` (if it doesn't exists) and the following variables to it:
    ```
    VITE_API_URL=http://localhost:8080
    VITE_KINDE_URL=https://your-business.kinde.com
    VITE_KINDE_CLIENT_ID=your-kinde-client-id
    ```

2. Install npm dependencies:
    ```bash
    npm i
    ```

3. Start the client:
    ```bash
    npm run dev
    ```

4. Press `To Login` to start authentication with Kinde :)

## JWT Issuer Validation with JWK in .NET

In the end, using a JWK(S) to validate JWT issuer in .NET turned out to be simple and effortless.

The following code snippet demonstrates how to use JWKS from Kinde to validate JWTs in .NET:

```cs
var kindeUrl = "https://your-business.kinde.com";

// ...

// 1. Request JWKS from Kinde.
using HttpClient client = new();
var res = await client.GetAsync($"{kindeUrl}/.well-known/jwks");
var jsonStr = await res.Content.ReadAsStringAsync();
var jwks = new JsonWebKeySet(jsonStr);

// ...

// 2. Create JWT validation parameters with the JWKS.
var jwtValidationParams = new TokenValidationParameters()
{
    // Validate both, the issuer and its signing key.
    ValidateIssuer = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = kindeUrl,
    IssuerSigningKey = jwks.Keys.First() // <-- The first JWK is used here!
    // ...
};

// ...

// 3. Use the JWT validation parameters.
// (This repo uses ASP.NET Core middleware to validate JWTs.)
```
