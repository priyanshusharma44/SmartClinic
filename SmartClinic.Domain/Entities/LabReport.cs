using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Domain.Entities
{
    public class LabReport
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid LabTechnicianId { get; set; }
        public string ReportType { get; set; } = null!; // BloodTest, XRay, etc.  
        public string Result { get; set; } = null!;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public Patient Patient { get; set; } = null!;
        public User LabTechnician { get; set; } = null!;
    }
}