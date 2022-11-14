using MongoDB.Driver;
using SeBackend.Common.Configurations;
using SeBackend.Common.Models;

namespace product_sv.Models
{
  public static class SeedData
  {
    public static void Seed(IConfiguration configuration)
    {
      MongodbConfig mongodbConfig = configuration.GetMongodbConfig();
      var mongoClient = new MongoClient(mongodbConfig.ConnectionString);
      var mongoDb = mongoClient.GetDatabase(mongodbConfig.DatabaseName);
      
      IMongoCollection<Product> _product = mongoDb.GetCollection<Product>(
          mongodbConfig.CollectionName
      );

      if(_product.CountDocuments(p => true) == 0)
      {
        string[] productNames = { "Orange", "Apple", "Papaya", "Durian" };
        int[] prices = { 100, 200, 50, 500 };
        IList<Product> products = new List<Product>();
        for (int i = 0; i < productNames.Length; i++)
        {
          products.Add(
              new Product
              {
                Name = productNames[i],
                Price = prices[i]
              }
          );
        }

        try
        {

          _product.InsertMany(products);


          Console.WriteLine("Successfully seeding Product!");
        }
        catch (Exception e)
        {
          Console.WriteLine("Failed seeding Product!");
          Console.WriteLine(e.Message);
        }
      }else{
        Console.WriteLine("Product exist!!");
      }
    }
  }
}