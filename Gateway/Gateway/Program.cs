using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

 builder.Configuration.AddJsonFile($"configuration.{Host.CreateApplicationBuilder().Environment.EnvironmentName.ToLower()}.json"); // hatal�. a�a��daki �al���yor ama dinamik hale getirilmeli.
builder.Configuration.AddJsonFile("ocelot.development.json");

builder.Services.AddOcelot();

var app = builder.Build();

 app.UseOcelot();



app.Run();