using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Applies to all endpoints in this controller
    public class AdminController : ControllerBase
    {
        [HttpGet("dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok("Welcome to Admin Dashboard!");
        }

        [HttpGet("system-stats")]
        public IActionResult GetSystemStatistics()
        {
            // Example stats
            var stats = new
            {
                TotalUsers = 150,
                ActiveAppointments = 45,
                Clinics = 5
            };
            return Ok(stats);
        }

        // New endpoint for admins
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("This is accessible only by Admins!");
        }
    }
}
