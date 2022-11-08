using Microsoft.AspNetCore.Mvc;
using order_sv.Interfaces;
using order_sv.Models;
using SeBackend.Common.Models;
using Newtonsoft.Json;

namespace order_sv.Services
{
  public class OrderService : ControllerBase, IOrderService
  {
    private readonly OrderContext context;
    private readonly IConfiguration configuration;

    public OrderService(OrderContext context, IConfiguration configuration)
    {
      this.context = context;
      this.configuration = configuration;
    }
    public ActionResult<IEnumerable<Order>> Get()
    {
      IEnumerable<Order> orders = context.Orders;

      IEnumerable<Product>? products = getProduct();
      

      foreach(Order order in orders)
      {
        IEnumerable<Product>? calProd = products!.Where(p => p.ProductId % 2 == 0);
        order.Products.AddRange(calProd);
        order.Amount=calProd.Select(p => p.Price).Sum();
      }
      return Ok(orders);
    }

    public IEnumerable<Product>? getProduct(){
            string url = $"{configuration["Services:ProductService"]}/api/products";
            var handler = new HttpClientHandler() 
            { 
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            HttpClient client = new HttpClient(handler);
            HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();
            string content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(content);
        }
  }
}