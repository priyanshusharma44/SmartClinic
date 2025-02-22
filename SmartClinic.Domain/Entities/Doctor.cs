using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SmartClinic.Domain/Entities/Doctor.cs  
namespace SmartClinic.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string Availability { get; set; } = null!; // JSON format: { "Monday": ["09:00-11:00"], ... }  
    }
}