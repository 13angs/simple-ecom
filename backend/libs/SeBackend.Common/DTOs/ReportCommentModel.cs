using Newtonsoft.Json;
using SeBackend.Common.Interfaces;
using SeBackend.Common.Models;

namespace SeBackend.Common.DTOs
{
  public class ReportCommentModel : Comment, IReport
  {
    [JsonProperty("event_name")]
    public string? EventName { get; set; }
  }
}