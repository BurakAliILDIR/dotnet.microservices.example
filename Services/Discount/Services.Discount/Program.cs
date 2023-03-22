using Services.Discount.Service;
using Shared.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDiscountService, DiscountService>();

// TODO: Bunun sayesinde userId veya request ve response bilgisine eriþebiliyoruz.
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();


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