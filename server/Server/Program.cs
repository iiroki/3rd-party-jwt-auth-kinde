using System.IdentityModel.Tokens.Jwt;
using IiroKi.Server.Config;
using IiroKi.Server.Middleware;

// ===== Configuration: =====
var builder = WebApplication.CreateBuilder(args);
var jwtValidationParams = Init.CreateJwtValidationParamsAsync(builder).GetAwaiter().GetResult();

// CORS (allow everything for demo purposes)
builder.Services.AddCors(
    opt => opt.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
);

// Setup JWT auth
builder.Services
    .AddAuthentication()
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = jwtValidationParams;
        Init.SetupJwtHandler((JwtSecurityTokenHandler)opt.SecurityTokenValidators.First());
    });

builder.Services.AddScoped<UserRoleMiddleware>();
builder.Services.AddControllers();

// ===== Request pipeline: =====
var app = builder.Build();
app.UseCors();
app.UseMiddleware<UserRoleMiddleware>();
app.UseAuthorization();
app.MapControllers();

// ===== Let's go! =====
app.Run();
