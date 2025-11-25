using System.Text.Json.Serialization;

namespace Levi9Challenge.Models
{
    public class Student
    {
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("isAdmin")]
        [JsonRequired]
        public bool IsAdmin { get; set; }
    }
}
