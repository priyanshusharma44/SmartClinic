using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// SmartClinic.Domain/Entities/Appointment.cs  
using SmartClinic.Domain.Enums;

namespace SmartClinic.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public bool IsEmergency { get; set; }
        public string Symptoms { get; set; } = null!; // For AI analysis  
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
    }
}
