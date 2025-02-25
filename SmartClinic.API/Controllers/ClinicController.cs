using Microsoft.AspNetCore.Mvc;
using SmartClinic.Domain.DTOs;
using SmartClinic.Domain.Entities;
using SmartClinic.Infrastructure.Data;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClinicController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClinic([FromBody] ClinicDto model)
        {
            var clinic = new Clinic
            {
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact
            };

            await _context.Clinics.AddAsync(clinic);
            await _context.SaveChangesAsync();

            return Ok(clinic.Id); // Return the generated ClinicId  
        }
    }

    public class ClinicDto
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Contact { get; set; } = null!;
    }
}