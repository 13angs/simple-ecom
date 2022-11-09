using Microsoft.AspNetCore.Mvc;
using order_sv.Interfaces;
using order_sv.Services;
using SeBackend.Common.Models;

namespace order_sv.Controllers
{
  [ApiController]
  [Route("api/order/orders")]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService orderService;

    public OrderController(IOrderService orderService)
    {
      this.orderService = orderService;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Order>> Get()
    {
      return orderService.Get();
    }

    [HttpGet]
    [Route("product/config")]
    public async Task<ActionResult> GetProductConfig()
    {
      var key = await ConsulKeyValueProvider.GetValueAsync<ProductConfig>(key: "product_service");
      return Ok(key);
    }
  }
}