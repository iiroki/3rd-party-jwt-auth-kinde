# 3rd Party JWT Auth with Kinde

A simple demonstration about how a 3rd party user authentication service, Kinde in this case,
can be used to authorize API requests between a React client and an ASP.NET Core web server with JSON Web Token (JWT).

First, the client must perform authentication with Kinde in order to gain access token (JWT).
The access token can be then be passed to API requests to the server,
which verifies the access token using JSON Web Key (JWK) from Kinde.

In total, the authentication/authorization consists of four parts:
1. The server requests JWK from Kinde in order to verify JWTs.
2. The client performs authentication with Kinde in order to gain access token (JWT).
3. The client makes an API request to the server, passing the access token as Bearer Token.
4. The server uses the JWK from part 1 to verify the JWT passed as Bearer Token.

TODO: Diagram

## Quickstart

**Both, the server and the client, must be started separately!**

### Kinde

TODO

### Server

1. Create `appsettings.Development.json` file under `server/Server/`
(if it doesn't exists) and add your Kinde URL to it:
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
