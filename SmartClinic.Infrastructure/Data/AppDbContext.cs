using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace SmartClinic.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Clinic> Clinics => Set<Clinic>();
        public DbSet<LabReport> LabReports => Set<LabReport>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Doctor-User (1:1)  
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient-User (1:1)  
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Clinic-Staff (1:Many)  
            modelBuilder.Entity<Clinic>()
                .HasMany(c => c.Staff)
                .WithOne(u => u.Clinic)
                .HasForeignKey(u => u.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment-Patient (Many:1)  
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment-Doctor (Many:1)  
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // LabReport-Patient (Many:1)  
            modelBuilder.Entity<LabReport>()
                .HasOne(l => l.Patient)
                .WithMany(p => p.LabReports)
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // LabReport-LabTechnician (Many:1)  
            modelBuilder.Entity<LabReport>()
                .HasOne(l => l.LabTechnician)
                .WithMany()
                .HasForeignKey(l => l.LabTechnicianId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment-Appointment (1:1)  
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Appointment)
                .WithOne()
                .HasForeignKey<Payment>(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
