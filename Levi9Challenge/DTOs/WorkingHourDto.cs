using System.Text.Json.Serialization;

namespace Levi9Challenge.DTOs
{
    public class WorkingHourDto
    {
        [JsonPropertyName("meal")]
        public string? Meal { get; set; }

        [JsonPropertyName("from")]
        public string? From { get; set; }

        [JsonPropertyName("to")]
        public string? To { get; set; }
    }
}
