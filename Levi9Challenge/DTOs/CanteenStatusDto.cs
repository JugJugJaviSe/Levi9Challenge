using System.Text.Json.Serialization;

namespace Levi9Challenge.DTOs
{
    public class CanteenStatusDto
    {
        [JsonPropertyName("canteenId")]
        public string CanteenId { get; set; }

        [JsonPropertyName("slots")]
        public List<CanteenSlotDto> Slots { get; set; } = new List<CanteenSlotDto>();
    }
}
