using System.Text.Json.Serialization;

namespace Backend.Models;

public interface IRequestErrorDTO
{
    [JsonPropertyName("response_code")]
    public string responseCode { get; set; }
    [JsonPropertyName("message")]
    public string message { get; set; }
    //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("stack_trace")]
    public string? stackTrace { get; set; } 
    public Guid errorId { get; set; }
}

public class RequestError : IRequestErrorDTO
{
    public string responseCode { get; set; }
    public string message { get; set; }
    public string? stackTrace { get; set; }
    public Guid errorId { get; set; } = Guid.NewGuid();
}