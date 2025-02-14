using OrderService.Clients;
using OrderService.Configurations;
using OrderService.Mappings;
using OrderService.Repositories;
using OrderService.Services;
using AutoMapper;
using OrderService.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Configuration de MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

// Assurez-vous que le fichier appsettings.json est bien chargé
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>();
builder.Services.AddHttpClient<ICustomerServiceClient, CustomerServiceClient>();
builder.Services.AddHttpClient<IInventoryServiceClient,InventoryServiceClient>();

// Configuration AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<KafkaConsumer>();
builder.Services.AddScoped<KafkaProducer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var kafkaConsumer = scope.ServiceProvider.GetRequiredService<KafkaConsumer>();
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
