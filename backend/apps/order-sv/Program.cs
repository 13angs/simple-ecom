using Consul;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using order_sv.Interfaces;
using order_sv.Models;
using order_sv.Services;
using SeBackend.Common.Configurations;
using SeBackend.Common.Models;
using SeBackend.Common.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IOrderService, OrderService>();
// var consulHost = "http://localhost:8500";
// builder.Services.AddConsulConfig(configKey: consulHost);
var serviceConfig = Configuration.GetServiceConfig();
builder.Services.RegisterConsulServices(serviceConfig);

// configure controller to use Newtonsoft as a default serializer
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver()
);

MongodbConfig mongodbConfig = Configuration.GetMongodbConfig();
builder.Services.AddSingleton(mongodbConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SeedData.Seed(app);

app.Run();
