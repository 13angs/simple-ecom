using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using product_sv.Interfaces;
using product_sv.Models;
using product_sv.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<ProductContext>(options => {
    options.UseInMemoryDatabase(Configuration["ConnectionStrings:InMemoryDb"]);
});

builder.Services.AddScoped<IProductService, ProductService>();

// configure controller to use Newtonsoft as a default serializer
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver()
);

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
