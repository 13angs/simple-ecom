using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using product_sv.Interfaces;
using SeBackend.Common.Models;
using SeBackend.Common.Services;

namespace product_sv.Services
{
  public class ProductService : ControllerBase, IProductService
  {
    private readonly IMongoCollection<Product?> _product;
    private readonly IConfiguration _configuration;

    public ProductService(IConfiguration configuration)
    {
      _configuration = configuration;
      _product = MongodbCollectionService.GetCollection<Product>(_configuration);

    }
    public ActionResult<IEnumerable<Product>> Get()
    {
      IEnumerable<Product?> products = _product.Find(p => true).ToList();
      return Ok(products);
    }
    public async Task<ActionResult<Product>> Post(Product product)
    {
      await _product.InsertOneAsync(product);
      return Ok(product);
    }
  }
}