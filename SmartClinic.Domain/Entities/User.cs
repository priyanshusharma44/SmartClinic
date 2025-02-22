// SmartClinic.Domain/Entities/User.cs  
using Microsoft.AspNetCore.Identity;
using SmartClinic.Domain.Enum;
using SmartClinic.Domain.Enums;

namespace SmartClinic.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; } = null!;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}