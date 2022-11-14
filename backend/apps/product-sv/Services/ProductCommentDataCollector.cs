using MongoDB.Driver;
using Newtonsoft.Json;
using product_sv.Interfaces;
using product_sv.Models;
using SeBackend.Common.DTOs;
using SeBackend.Common.Models;
using SeBackend.Common.Services;

namespace product_sv.Services
{
  public class ProductCommentDataCollector : IProductCommentDataCollector
  {
    private readonly ILogger<ProductCommentDataCollector> logger;
    private readonly IConfiguration configuration;
    private readonly IMongoCollection<Product?> _product;

    // private readonly ProductContext context;

    public ProductCommentDataCollector(ILogger<ProductCommentDataCollector> logger, IConfiguration configuration)
    {
      this.logger = logger;
      this.configuration = configuration;
      _product = MongodbCollectionService.GetCollection<Product?>(configuration);
      // this.context = context;
    }
    public bool Create(string message)
    {
      ReportCommentModel? model = ToModel(message);

      try
      {
        Product query = new Product{
          ProductId=model!.ProductId
        };
        Product? product = _product.Find(p => p!.ProductId == model!.ProductId).FirstOrDefault();
        
        product!.NComments += 1;
        _product.ReplaceOne(p => p!.ProductId == model.ProductId, product);
        logger.LogInformation("--> Product updated");  
          return true;      

      }
      catch (Exception e)
      {
        logger.LogError(e.Message);
        return false;
      }
    }

    public ReportCommentModel? ToModel(string message)
    {
      return JsonConvert.DeserializeObject<ReportCommentModel>(message);
    }
  }
}