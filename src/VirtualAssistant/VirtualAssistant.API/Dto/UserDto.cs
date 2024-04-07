using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VirtualAssistant.API.Models
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        [Required]
        public long Id { get; set; }

        [JsonPropertyName("username")]
        [MaxLength(256)]
        public string? Username { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
    }
}
