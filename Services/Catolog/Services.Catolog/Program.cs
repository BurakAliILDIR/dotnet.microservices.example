using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Options;
using Services.Catolog.Mapping;
using Services.Catolog.Service;
using Services.Catolog.Setting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// auto mapping
builder.Services.AddAutoMapper(typeof(GeneralMapping));

// appsettings.json dosyas�ndan de�erleri alabilmek i�in yap�lan ayar. Setting klas�r� i�inde par�as� var.
builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("DatabaseSetting"));
builder.Services.AddSingleton<IDatabaseSetting>(x => x.GetRequiredService<IOptions<DatabaseSetting>>().Value);

// category service
builder.Services.AddScoped<ICategoryService, CategoryService>();
// course service
builder.Services.AddScoped<ICourseService, CourseService>();

// mass transit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQ:Url"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:Username"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });
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