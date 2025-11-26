using Levi9Challenge.Models;
using System.Text.Json.Serialization;

namespace Levi9Challenge.DTOs
{
    public class ReservationDto
    {
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("studentId")]
        public string StudentId { get; set; }

        [JsonPropertyName("canteenId")]
        public string CanteenId { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

    }
}
