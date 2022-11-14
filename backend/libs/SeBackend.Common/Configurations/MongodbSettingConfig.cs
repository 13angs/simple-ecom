using Microsoft.Extensions.Configuration;
using SeBackend.Common.Models;

namespace SeBackend.Common.Configurations
{
  public static class MongodbConfigExtension
  {
    public static MongodbConfig GetMongodbConfig(this IConfiguration configuration)
    {
      if (configuration == null)
      {
        throw new ArgumentNullException(nameof(configuration));
      }

      return new MongodbConfig{
        ConnectionString=configuration.GetValue<string>("MongodbConfig:ConnectionString"),
        DatabaseName=configuration.GetValue<string>("MongodbConfig:DatabaseName"),
        CollectionName=configuration.GetValue<string>("MongodbConfig:CollectionName")
      };
    }
  }
}