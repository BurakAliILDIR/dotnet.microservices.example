using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");
builder.Configuration.AddJsonFile($"ocelot.development.json");

builder.Services.AddOcelot();

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthSchema",options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:JwtToken")
                .Value!))
    };
});

var app = builder.Build();

app.UseAuthorization();

app.UseOcelot();



app.Run();