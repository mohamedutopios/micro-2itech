using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repositories;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration d'Entity Framework Core avec PostgreSQL
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injection des dépendances pour le Repository Pattern
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();