using Newtonsoft.Json;
using product_sv.Interfaces;
using product_sv.Models;
using SeBackend.Common.DTOs;
using SeBackend.Common.Models;

namespace product_sv.Services
{
  public class ProductCommentDataCollector : IProductCommentDataCollector
  {
    private readonly ILogger<ProductCommentDataCollector> logger;
    // private readonly ProductContext context;

    public ProductCommentDataCollector(ILogger<ProductCommentDataCollector> logger)
    {
      this.logger = logger;
      // this.context = context;
    }
    public bool Create(string message)
    {
      ReportCommentModel? model = ToModel(message);

      try
      {
        // Product? product = context.Products
        //     .FirstOrDefault(p => p.ProductId == model!.ProductId);
        
        // if(product != null)
        // {
        //     product.NComments += 1;
            
        //     int result = context.SaveChanges();
        //     if(result > 0)
        //     {
        //         logger.LogInformation("--> Product updated");  
        //         return true;      
        //     }
        // }

        logger.LogWarning("--> Product not found");
        return false;
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