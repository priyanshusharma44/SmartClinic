using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SmartClinic.Domain/Entities/Patient.cs  
namespace SmartClinic.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string MedicalHistory { get; set; } = null!;
    }
}
