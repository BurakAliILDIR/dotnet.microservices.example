using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

 builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");

builder.Services.AddOcelot();

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthSchema", option =>
{
    option.Authority = "test";
    option.Audience = "test";
    option.RequireHttpsMetadata = false;
});


var app = builder.Build();

app.UseOcelot();


app.Run();