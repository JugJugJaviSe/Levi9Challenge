using System.Text.Json.Serialization;

namespace Levi9Challenge.DTOs
{
    public class CanteenDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("workingHours")]
        public List<WorkingHourDto>? WorkingHours { get; set; }
    }
}
