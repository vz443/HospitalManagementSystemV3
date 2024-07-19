using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HospitalManagementSystemV3.Database
{
    public class AppDbContext : DbContext
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
            var doctorId1 = Guid.NewGuid();
            var doctorId2 = Guid.NewGuid();
            var patientId1 = Guid.NewGuid();
            var patientId2 = Guid.NewGuid();
            var patientId3 = Guid.NewGuid();

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = doctorId1,
                    Name = "Dr. John Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St, Anytown, USA",
                    Username = "john_doe",
                    Password = "password123"
                },
                new Doctor
                {
                    Id = doctorId2,
                    Name = "Dr. Jane Smith",
                    Email = "jane.smith@example.com",
                    Phone = "098-765-4321",
                    Address = "456 Oak St, Anytown, USA",
                    Username = "jane_smith",
                    Password = "password123"
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = patientId1,
                    Name = "Patient One",
                    Email = "patient.one@example.com",
                    Phone = "111-222-3333",
                    Address = "789 Pine St, Anytown, USA",
                    Username = "patient_one",
                    Password = "password123",
                    DoctorId = doctorId1
                },
                new Patient
                {
                    Id = patientId2,
                    Name = "Patient Two",
                    Email = "patient.two@example.com",
                    Phone = "444-555-6666",
                    Address = "321 Elm St, Anytown, USA",
                    Username = "patient_two",
                    Password = "password123",
                    DoctorId = doctorId1 
                },
                new Patient
                {
                    Id = patientId3,
                    Name = "Patient Three",
                    Email = "patient.three@example.com",
                    Phone = "777-888-9999",
                    Address = "654 Birch St, Anytown, USA",
                    Username = "patient_three",
                    Password = "password123",
                    DoctorId = doctorId2 
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    DoctorId = doctorId1,
                    PatientId = patientId1,
                    Description = "General Checkup"
                },
                new Appointment
                {
                    Id = 2,
                    DoctorId = doctorId1,
                    PatientId = patientId2,
                    Description = "Follow-up Visit"
                },
                new Appointment
                {
                    Id = 3,
                    DoctorId = doctorId2,
                    PatientId = patientId3,
                    Description = "Consultation"
                }
            );
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
