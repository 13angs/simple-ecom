using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class Comment : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("comment_id")]
        [JsonProperty("comment_id")]
        public string? CommentId { get; set; }

        [BsonElement("product_id")]
        [JsonProperty("product_id")]
        public string? ProductId { get; set; }

        [BsonElement("text")]
        [JsonProperty("text")]
        public string? Text { get; set; }

        [BsonElement("nlikes")]
        [JsonProperty("nlikes")]
        public int NLikes { get; set; }
    }
}