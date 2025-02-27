using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001";
        // Audience validation is disabled here because access to the API is modeled with ApiScopes only.
        // By default, no Audience will be emitted unless the API is modeled with ApiResources instead.
        options.TokenValidationParameters.ValidateAudience = false;
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("identity", (ClaimsPrincipal user) => user.Claims.Select(c => new { c.Type, c.Value }))
    .RequireAuthorization();

app.MapGet("/public", () => ( "Hello World!" ));

app.Run();
