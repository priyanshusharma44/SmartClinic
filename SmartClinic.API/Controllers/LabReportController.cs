// SmartClinic.API/Controllers/LabReportController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Domain.DTOs;
using SmartClinic.Domain.Entities;
using SmartClinic.Infrastructure.Data;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LabReportController(AppDbContext context) => _context = context;

        // POST: api/labreport
        [HttpPost]
        [Authorize(Roles = "LabTechnician")]
        public async Task<IActionResult> GenerateReport(LabReportDto dto)
        {
            var report = new LabReport
            {
                PatientId = dto.PatientId,
                LabTechnicianId = dto.LabTechnicianId,
                ReportType = dto.ReportType,
                Result = dto.Result
            };

            _context.LabReports.Add(report);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: api/labreport/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        [Authorize(Roles = "Patient,Doctor")]
        public async Task<ActionResult<IEnumerable<LabReportResponseDto>>> GetPatientReports(Guid patientId)
        {
            return await _context.LabReports
                .Include(r => r.Patient.User)
                .Include(r => r.LabTechnician)
                .Where(r => r.PatientId == patientId)
                .Select(r => new LabReportResponseDto
                {
                    Id = r.Id,
                    PatientName = r.Patient.User.FullName,
                    LabTechnicianName = r.LabTechnician.FullName,
                    ReportType = r.ReportType,
                    Result = r.Result,
                    GeneratedAt = r.GeneratedAt
                })
                .ToListAsync();
        }
    }
}