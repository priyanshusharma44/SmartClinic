using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartClinic.Domain.DTOs;
using SmartClinic.Domain.Entities;
using SmartClinic.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SmartClinic.Infrastructure.Data;

using System.Text;
using System;

namespace SmartClinic.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthService(
            UserManager<User> userManager,
            IConfiguration configuration,
            AppDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        // SmartClinic.Application/Services/AuthService.cs  
        public async Task<string> RegisterAsync(RegisterDto model)
        {
            // Validate ClinicId (if provided)  
            if (model.ClinicId != null && !await _context.Clinics.AnyAsync(c => c.Id == model.ClinicId))
                throw new Exception("Invalid ClinicId");

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                Role = model.Role,
                ClinicId = model.ClinicId
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new Exception($"Registration failed: {string.Join(", ", result.Errors)}");

            await _userManager.AddToRoleAsync(user, model.Role.ToString());
            return "User registered successfully!";
        }
        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                throw new Exception("Invalid email or password");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("ClinicId", user.ClinicId?.ToString() ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}