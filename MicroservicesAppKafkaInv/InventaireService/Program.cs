using InventaireService.Data;
using InventaireService.Kafka;
using InventaireService.Repositories;
using InventaireService.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5005");

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseInMemoryDatabase("InventoryDb"));
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventorysService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<InventoryConsumer>();

var app = builder.Build();


Console.WriteLine("API InventoryService démarrée...");

using (var scope = app.Services.CreateScope())
{
    var kafkaConsumer = scope.ServiceProvider.GetRequiredService<InventoryConsumer>();
    Task.Run(() => kafkaConsumer.StartConsuming());
}

app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();