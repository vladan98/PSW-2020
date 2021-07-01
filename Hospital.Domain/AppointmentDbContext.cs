using Hospital.Domain.Models;
using Hospital.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Center.Repository
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }



    }
}
