
using Newtonsoft.Json;

namespace SeBackend.Common.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate=DateTime.Now;
            ModifiedDate=DateTime.Now;
        }
        
        [JsonProperty("Created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}