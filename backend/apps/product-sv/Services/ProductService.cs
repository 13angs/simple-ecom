using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using product_sv.Interfaces;
using SeBackend.Common.Models;

namespace product_sv.Services
{
  public class ProductService : ControllerBase, IProductService
  {
    private readonly IMongoCollection<Product> _product;

    public ProductService(MongodbConfig mongodbConfig)
    {
      var mongoClient = new MongoClient(mongodbConfig.ConnectionString);
      var mongoDb = mongoClient.GetDatabase(mongodbConfig.DatabaseName);
      _product = mongoDb.GetCollection<Product>(
        mongodbConfig.CollectionName
      );
    }
    public ActionResult<IEnumerable<Product>> Get()
    {
      IEnumerable<Product> products = _product.Find(p => true).ToList();
      return Ok(products);
    }
    public async Task<ActionResult<Product>> Post(Product product)
    {
      await _product.InsertOneAsync(product);
      return Ok(product);
    }
  }
}