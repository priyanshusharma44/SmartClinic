using SmartClinic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// SmartClinic.Domain/DTOs/RegisterDto.cs  
namespace SmartClinic.Domain.DTOs
{
    public class RegisterDto
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

        [JsonPropertyName("clinicId")]
        public Guid? ClinicId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("role")]
        public UserRole Role { get; set; }
    }
}
