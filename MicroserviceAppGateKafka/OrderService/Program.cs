using OrderService.Clients;
using OrderService.Configurations;
using OrderService.Mappings;
using OrderService.Repositories;
using OrderService.Services;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ConfigService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8888/");
});

var serviceProvider = builder.Services.BuildServiceProvider();
var configService = serviceProvider.GetRequiredService<ConfigService>();


var mongoDbSettings = await configService.GetMongoDbSettingsAsync("order-service");

builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = mongoDbSettings.ConnectionString;
    options.DatabaseName = mongoDbSettings.DatabaseName;
});


builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();


builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>();
builder.Services.AddHttpClient<ICustomerServiceClient, CustomerServiceClient>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<KafkaConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
