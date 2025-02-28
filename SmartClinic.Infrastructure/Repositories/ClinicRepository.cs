// SmartClinic.Infrastructure/Repositories/ClinicRepository.cs

using Microsoft.EntityFrameworkCore;
using SmartClinic.Application.Interfaces;
using SmartClinic.Infrastructure.Data;
using System;

namespace SmartClinic.Infrastructure.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly AppDbContext _context;

        public ClinicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ClinicExists(Guid clinicId)
        {
            return await _context.Clinics.AnyAsync(c => c.Id == clinicId);
        }
    }
}