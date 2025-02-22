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
            public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

            public DbSet<User> Users => Set<User>();
            public DbSet<Doctor> Doctors => Set<Doctor>();
        
            public DbSet<Patient> Patients => Set<Patient>();

            public DbSet<Appointment> Appointments => Set<Appointment>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure Doctor-User relationship (1:1)  
            modelBuilder.Entity<Doctor>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

            //Configure Patient-user relationship (1:1)
            modelBuilder.Entity<Patient>()
            .HasOne(p=> p.User)
            .WithOne()
            .HasForeignKey< Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

            //Configuration Appointment-Patient Relationship (Many:One)
            modelBuilder.Entity<Appointment>()
            .HasOne(a=> a.Patient)
            .WithMany()
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

            // Configure Appointment-Doctor relationship (Many:1)  
            modelBuilder.Entity<Appointment>()  
            .HasOne(a=> a.Doctor)
            .WithMany()
            .HasForeignKey(a=>a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete
        }
    }
}
