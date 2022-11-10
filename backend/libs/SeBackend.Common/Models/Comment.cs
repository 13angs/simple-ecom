using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class Comment : BaseEntity
    {

        [JsonProperty("comment_id")]
        public int CommentId { get; set; }

        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("nlikes")]
        public int NLikes { get; set; }
    }
}