using Microsoft.AspNetCore.Mvc;
using product_sv.Interfaces;
using SeBackend.Common.Models;

namespace product_sv.Controllers
{
  [ApiController]
  [Route("api/product/products")]
  public class ProductController : ControllerBase
  {
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
      this.productService = productService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        return productService.Get();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Post([FromBody] Product product)
    {
        return await productService.Post(product);
    }
  }
}