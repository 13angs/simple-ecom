using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using product_sv.BgService;
using product_sv.Interfaces;
using product_sv.Models;
using product_sv.Services;
using SeBackend.Common.Configurations;
using SeBackend.Common.Interfaces;
using SeBackend.Common.Models;
using SeBackend.Common.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
var serviceConfig = Configuration.GetServiceConfig();
builder.Services.RegisterConsulServices(serviceConfig);
builder.Services.AddScoped<IMessageSubscriber, MessageSubscriber>();
builder.Services.AddScoped<IProductCommentDataCollector, ProductCommentDataCollector>();
builder.Services.AddHostedService<DataCollector>();
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
    SeedData.Seed(Configuration);
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
