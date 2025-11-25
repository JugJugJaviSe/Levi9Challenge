using System.Text.Json.Serialization;

namespace Levi9Challenge.DTOs
{
    public class CanteenSlotDto
    {
        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }

        [JsonPropertyName("meal")]
        public string Meal { get; set; }

        [JsonPropertyName("startTime")]
        public TimeSpan StartTime { get; set; }

        [JsonPropertyName("remainingCapacity")]
        public int RemainingCapacity { get; set; }
    }
}
