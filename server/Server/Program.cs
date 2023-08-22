using Microsoft.AspNetCore.Authentication.JwtBearer;
using IiroKi.Server.Config;

// ===== Configuration: =====
var builder = WebApplication.CreateBuilder(args);
var jwtValidationParams = Init.CreateJwtValidationParamsAsync(builder).GetAwaiter().GetResult();

// Setup JWT auth
builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = jwtValidationParams;
        opt.MapInboundClaims = false; // Do not alter claims!
    });


builder.Services.AddControllers();

// ===== Request pipeline: =====
var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

// ===== Let's go! =====
app.Run();
