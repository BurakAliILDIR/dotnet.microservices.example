using AutoMapper;
using Microsoft.Extensions.Options;
using Services.Catolog.Service;
using Services.Catolog.Setting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// auto mapping
builder.Services.AddAutoMapper(typeof(Profile));

// appsettings.json dosyasýndan deðerleri alabilmek için yapýlan ayar. Setting klasörü içinde parçasý var.
builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("DatabaseSetting"));
builder.Services.AddSingleton<IDatabaseSetting>(x => x.GetRequiredService<IOptions<DatabaseSetting>>().Value);

// category service
builder.Services.AddScoped<ICategoryService, CategoryService>();
// course service
builder.Services.AddScoped<ICourseService, CourseService>();

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