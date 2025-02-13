using System.Configuration;
using CustomerService.Data;
using CustomerService.Kafka;
using CustomerService.Repositories;
using CustomerService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<ConfigService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8888/");
});


var serviceProvider = builder.Services.BuildServiceProvider();
var configService = serviceProvider.GetRequiredService<ConfigService>();


var connectionString = await configService.GetConnectionStringAsync("customer-service");


builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddSingleton<KafkaProducer>();

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