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

    [HttpPost]
    public async Task<ActionResult<Order>> Post([FromBody] Order order)
    {
      return await orderService.Post(order);
    }

  }
}