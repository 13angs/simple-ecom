using Microsoft.AspNetCore.Mvc;
using product_sv.Interfaces;
using product_sv.Models;
using SeBackend.Common.Models;

namespace product_sv.Services
{
  public class ProductService : ControllerBase, IProductService
  {
    private readonly ProductContext context;

    public ProductService(ProductContext context)
    {
      this.context = context;
    }
    public ActionResult<IEnumerable<Product>> Get()
    {
      IEnumerable<Product> products = context.Products;
      return Ok(products);
    }
  }
}