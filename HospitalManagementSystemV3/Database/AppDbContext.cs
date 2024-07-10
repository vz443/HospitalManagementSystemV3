using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HospitalManagementSystemV3.Database
{
    class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            CreateDatabase();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        public string ?DbPath { get; private set; }

        void CreateDatabase()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "hospital.db");
        }

        public void ClearDatabase()
        {
            Patients.RemoveRange(Patients);
            Doctors.RemoveRange(Doctors);
            Appointments.RemoveRange(Appointments);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
