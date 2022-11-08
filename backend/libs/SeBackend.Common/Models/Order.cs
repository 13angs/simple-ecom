using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }

        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("products")]
        public List<Product> Products { get; set; }
    }
}