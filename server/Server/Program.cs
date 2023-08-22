using IiroKi.Server.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// ===== Configuration: =====
var builder = WebApplication.CreateBuilder(args);
var jwtValidationParams = Init.CreateJwtValidationParamsAsync(builder).GetAwaiter().GetResult();

// CORS (allow everything for demo purposes)
builder.Services.AddCors(
    opt => opt.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
);

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
app.UseCors();
app.UseAuthorization();
app.MapControllers();

// ===== Let's go! =====
app.Run();
