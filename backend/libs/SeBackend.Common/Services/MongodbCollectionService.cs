using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SeBackend.Common.Configurations;
using SeBackend.Common.Models;

namespace SeBackend.Common.Services
{
  public static class MongodbCollectionService
  {
    public static IMongoCollection<T?> GetCollection<T>(IConfiguration configuration)
    {
        MongodbConfig mongodbConfig = configuration.GetMongodbConfig();
        var mongoClient = new MongoClient(mongodbConfig.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(mongodbConfig.DatabaseName);
        
        IMongoCollection<T?> _context = mongoDb.GetCollection<T?>(
            mongodbConfig.CollectionName
        );
      return _context;
    }
  }
}