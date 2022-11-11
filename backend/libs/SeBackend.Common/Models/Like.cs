using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class Like : BaseEntity
    {
        [JsonProperty("like_id")]
        public int LikeId { get; set; }

        [JsonProperty("ref_id")]
        public int RefId { get; set; }

        [JsonProperty("ref_name")]
        public string? RefName { get; set; }
    }
}