// SmartClinic.Infrastructure/Data/DbSeeder.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Domain.Entities;
using SmartClinic.Domain.Enums;
using SmartClinic.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedTestData(AppDbContext context, UserManager<User> userManager)
    {
        // 1️⃣ Ensure Clinic Exists First
        Clinic clinic = await context.Clinics.FirstOrDefaultAsync(c => c.Name == "Main City Hospital");

        if (clinic == null)
        {
            clinic = new Clinic
            {
                Name = "Main City Hospital",
                Address = "123 Healthcare Street",
                Contact = "555-1234"
            };

            context.Clinics.Add(clinic);
            await context.SaveChangesAsync(); // 🚀 Save first to generate ID
        }

        // 2️⃣ Now Create Admin User with Clinic ID
        if (!userManager.Users.Any(u => u.Email == "admin@clinic.com"))
        {
            var adminUser = new User
            {
                FullName = "Admin User",
                Email = "admin@clinic.com",
                UserName = "admin@clinic.com",
                Role = UserRole.Admin,
                ClinicId = clinic.Id // ✅ Ensured ClinicId is valid
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@1234!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, UserRole.Admin.ToString());
            }
        }

        // 3️⃣ Same Fix for Lab Technician
        if (!userManager.Users.Any(u => u.Email == "lab@clinic.com"))
        {
            var labTechUser = new User
            {
                FullName = "Lab Technician",
                Email = "lab@clinic.com",
                UserName = "lab@clinic.com",
                Role = UserRole.LabTechnician,
                ClinicId = clinic.Id // ✅ Valid ClinicId
            };

            var result = await userManager.CreateAsync(labTechUser, "Lab@1234!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(labTechUser, UserRole.LabTechnician.ToString());
            }
        }

        // 4️⃣ Seed Patient (No Clinic Required)
        if (!userManager.Users.Any(u => u.Email == "patient@clinic.com"))
        {
            var patientUser = new User
            {
                FullName = "John Doe",
                Email = "patient@clinic.com",
                UserName = "patient@clinic.com",
                Role = UserRole.Patient
            };

            var result = await userManager.CreateAsync(patientUser, "Patient@1234!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(patientUser, UserRole.Patient.ToString());

                // Associate patient entity
                var patient = new Patient
                {
                    UserId = patientUser.Id,
                    MedicalHistory = "No significant medical history"
                };

                context.Patients.Add(patient);
                await context.SaveChangesAsync();
            }
        }
    }
}
