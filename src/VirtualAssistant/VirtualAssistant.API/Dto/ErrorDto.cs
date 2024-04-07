using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VirtualAssistant.API.Models
{
    public class ErrorDto
    {
        [JsonPropertyName("code")]
        [Required]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        [Required]
        public string Message { get; set; } = default!;
    }
}
