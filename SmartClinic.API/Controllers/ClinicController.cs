// SmartClinic.API/Controllers/ClinicController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Domain.Entities;
using SmartClinic.Infrastructure.Data;
using SmartClinic.API.Models;  // Assuming ClinicDto is defined in the Models folder

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ClinicController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClinicController(AppDbContext context) => _context = context;

        // GET: api/clinic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinics() =>
            await _context.Clinics.ToListAsync();

        // POST: api/clinic
        [HttpPost]
        public async Task<IActionResult> CreateClinic([FromBody] ClinicDto model)
        {
            try
            {
                var clinic = new Clinic
                {
                    Name = model.Name,
                    Address = model.Address,
                    Contact = model.Contact
                };

                await _context.Clinics.AddAsync(clinic);
                await _context.SaveChangesAsync();

                return Ok(new { clinic.Id }); // Return only the clinic Id
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Handle unexpected errors
            }
        }

        // DELETE: api/clinic/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(Guid id)
        {
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null) return NotFound();

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
