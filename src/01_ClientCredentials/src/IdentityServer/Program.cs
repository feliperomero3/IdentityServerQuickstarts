using IdentityServer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

Log.Information("Starting up");

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();
