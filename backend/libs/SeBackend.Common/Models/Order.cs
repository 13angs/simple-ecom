using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("order_id")]
        [JsonProperty("order_id")]
        public string? OrderId { get; set; }

        [BsonElement("amount")]
        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [BsonElement("products")]
        [JsonProperty("products")]
        public List<Product>? Products { get; set; }
    }
}