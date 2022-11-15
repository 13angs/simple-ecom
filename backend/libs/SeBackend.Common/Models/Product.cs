using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("product_id")]
        [JsonProperty("product_id")]
        public string? ProductId { get; set; }

        [BsonElement("price")]
        [JsonProperty("price")]
        public int Price { get; set; }

        [BsonElement("name")]
        [JsonProperty("name")]
        public string? Name { get; set; }
        
        [BsonElement("n_comments")]
        [JsonProperty("n_comments")]
        public int NComments { get; set; }
    }
}