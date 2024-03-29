using System.Text;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Order.Application.Consumer;
using Services.Order.Application.Handler;
using Services.Order.Infrastructure;
using Shared.Service;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// mediatR
builder.Services.AddMediatR(typeof(GetOrdersByUserIdHandler).Assembly);

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddHttpContextAccessor(); // SharedIdentityService kulland��� i�in eklendi.

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        configure => { configure.MigrationsAssembly("Services.Order.Infrastructure"); });
});


builder.Services.AddAuthentication().AddJwtBearer(options =>
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


// mass transit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateOrderMessageCommandConsumer>(); // Consumer eklendi.
    x.AddConsumer<CourseNameChangedEventConsumer>(); // Consumer eklendi.

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ:Url"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:Username"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        config.ReceiveEndpoint("create-order-service", e => e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context));

        config.ReceiveEndpoint("course-name-changed-event-order-service", e => e.ConfigureConsumer<CourseNameChangedEventConsumer>(context));
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();