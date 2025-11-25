using System.Text.Json.Serialization;

namespace Levi9Challenge.Models
{
    public class Canteen
    {
        public string? Id { get; set; } = "0";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("workingHours")]
        public List<WorkingHours> WorkingHours { get; set; } = new List<WorkingHours>();

    }
}
