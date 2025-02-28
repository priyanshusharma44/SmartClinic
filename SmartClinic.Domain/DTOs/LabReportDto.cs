// SmartClinic.Domain/DTOs/LabReportDto.cs
namespace SmartClinic.Domain.DTOs
{
    public class LabReportDto
    {
        public Guid PatientId { get; set; }
        public Guid LabTechnicianId { get; set; } // Will be auto-set from the logged-in user
        public string ReportType { get; set; } = null!;
        public string Result { get; set; } = null!;
    }

    public class LabReportResponseDto
    {
        public Guid Id { get; set; }
        public string PatientName { get; set; } = null!;
        public string LabTechnicianName { get; set; } = null!;
        public string ReportType { get; set; } = null!;
        public string Result { get; set; } = null!;
        public DateTime GeneratedAt { get; set; }
    }
}