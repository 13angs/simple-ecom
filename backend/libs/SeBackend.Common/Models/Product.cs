using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class Product
    {
        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}