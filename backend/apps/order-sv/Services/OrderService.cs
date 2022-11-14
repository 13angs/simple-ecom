using Microsoft.AspNetCore.Mvc;
using order_sv.Interfaces;
using order_sv.Models;
using SeBackend.Common.Models;
using Newtonsoft.Json;
using MongoDB.Driver;
using SeBackend.Common.Services;

namespace order_sv.Services
{
  public class OrderService : ControllerBase, IOrderService
  {
    private readonly IConfiguration configuration;
    private readonly ILogger<OrderService> logger;
    private readonly IMongoCollection<Order?> _order;

    public OrderService(IConfiguration configuration, ILogger<OrderService> logger)
    {
      this.configuration = configuration;
      this.logger = logger;
      _order = MongodbCollectionService.GetCollection<Order>(configuration);
    }
    public ActionResult<IEnumerable<Order>> Get()
    {
      IEnumerable<Order?> orders = _order.Find(o => true).ToList();

      // IEnumerable<Product>? products = getProduct();

      // foreach (Order? order in orders)
      // {
      //   IEnumerable<Product>? calProd = products!.Where(p => p.ProductId % 2 == 0);
      //   order!.Products!.AddRange(calProd);
      //   order.Amount = calProd.Select(p => p.Price).Sum();
      // }

      return Ok(orders);
    }

    public async Task<ActionResult<Order>> Post(Order order)
    {
      try{
        int total = order.Products!.Select(p => p.Price).Sum();
        order.Amount = total;
        await _order.InsertOneAsync(order);
        logger.LogInformation("--> Order created!");
        
        return StatusCode(StatusCodes.Status201Created, order);
      }catch (Exception e)
      {
        logger.LogError(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    public IEnumerable<Product>? getProduct()
    {
      string host = configuration["Services:ProductService"];
      string url = $"{host}/api/product/products";
      Console.WriteLine($"--> Host: {url}");
      var handler = new HttpClientHandler()
      {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      };
      HttpClient client = new HttpClient(handler);
      HttpResponseMessage res = client.GetAsync(url).GetAwaiter().GetResult();
      string content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
      Console.WriteLine(res.StatusCode);
      Console.WriteLine(content);
      return JsonConvert.DeserializeObject<IEnumerable<Product>>(content);
    }
  }
}