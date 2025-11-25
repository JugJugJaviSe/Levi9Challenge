using System.Text.Json.Serialization;

namespace Levi9Challenge.Models
{
    public class WorkingHours
    {
        [JsonPropertyName("meal")]
        public string Meal { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }
    }
}
