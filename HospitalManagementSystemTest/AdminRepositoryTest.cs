using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using HospitalManagementSystemV3.App.Repository;
using NUnit.Framework;

namespace HospitalManagementSystemTest
{
    public class AdminRepositoryTest
    {
        private Mock<AppDbContext>? _mockContext;
        private AdminRepository? _adminRepository;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<AppDbContext>();

            // Mock DbSet properties
            var mockAdmins = new Mock<DbSet<Admin>>();
            var mockPatients = new Mock<DbSet<Patient>>();
            var mockDoctors = new Mock<DbSet<Doctor>>();

            _mockContext.Setup(c => c.Admins).Returns(mockAdmins.Object);
            _mockContext.Setup(c => c.Patients).Returns(mockPatients.Object);
            _mockContext.Setup(c => c.Doctors).Returns(mockDoctors.Object);

            // Create repository instance
            _adminRepository = new AdminRepository(_mockContext.Object);
        }


        [Test]
        public void GetAllPatients_ReturnsAllPatients()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", Phone = "123456", Address = "123 Main St", Username = "john", Password = "password" },
                new Patient { Id = Guid.NewGuid(), Name = "Jane Doe", Email = "jane@example.com", Phone = "789101", Address = "456 Main St", Username = "jane", Password = "password" }
            }.AsQueryable();

            var mockPatientsDbSet = new Mock<DbSet<Patient>>();
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

            _mockContext.Setup(c => c.Patients).Returns(mockPatientsDbSet.Object);

            // Act
            var result = _adminRepository.GetAllPatients();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAllDoctors_ReturnsAllDoctors()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. Smith", Email = "smith@example.com", Phone = "123456", Address = "123 Main St", Username = "drsmith", Password = "password" },
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. Doe", Email = "doe@example.com", Phone = "789101", Address = "456 Main St", Username = "drdoe", Password = "password" }
            }.AsQueryable();

            var mockDoctorsDbSet = new Mock<DbSet<Doctor>>();
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(doctors.Provider);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(doctors.Expression);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(doctors.ElementType);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(doctors.GetEnumerator());

            _mockContext.Setup(c => c.Doctors).Returns(mockDoctorsDbSet.Object);

            // Act
            var result = _adminRepository.GetAllDoctors();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetPatientById_ReturnsPatient()
        {
            // Arrange
            var patientId = "john";
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john@example.com",
                Phone = "123456",
                Address = "123 Main St",
                Username = "john",
                Password = "password",
                Doctor = new Doctor { Id = Guid.NewGuid(), Name = "Dr. A", Email = "a@example.com", Phone = "123456", Address = "Address A", Username = "dr.a", Password = "passA" },
                Appointments = new List<Appointment>
                {
                    new Appointment { Id = Guid.NewGuid(), PatientId = Guid.NewGuid(), DoctorId = Guid.NewGuid(), Description = "Check-up"}
                }
            };

            var mockPatientsDbSet = new Mock<DbSet<Patient>>();
            var patientData = new List<Patient> { patient }.AsQueryable();

            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patientData.Provider);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patientData.Expression);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patientData.ElementType);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patientData.GetEnumerator());

            _mockContext.Setup(c => c.Patients).Returns(mockPatientsDbSet.Object);

            // Act
            var result = _adminRepository.GetPatientById(patientId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(patientId, result.Username);
        }

        [Test]
        public void GetDoctorById_ReturnsDoctor()
        {
            // Arrange
            var doctorId = "drsmith";
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = "Dr. Smith",
                Email = "smith@example.com",
                Phone = "123456",
                Address = "123 Main St",
                Username = "drsmith",
                Password = "password",
                Patients = new List<Patient>
                {
                    new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", Phone = "123456", Address = "123 Main St", Username = "john", Password = "password" }
                }
            };

            var mockDoctorsDbSet = new Mock<DbSet<Doctor>>();
            var doctorData = new List<Doctor> { doctor }.AsQueryable();

            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(doctorData.Provider);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(doctorData.Expression);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(doctorData.ElementType);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(doctorData.GetEnumerator());

            _mockContext.Setup(c => c.Doctors).Returns(mockDoctorsDbSet.Object);

            // Act
            var result = _adminRepository.GetDoctorById(doctorId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(doctorId, result.Username);
        }

        [Test]
        public void RemoveAdmin_RemovesAdmin()
        {
            // Arrange
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "Admin1",
                Email = "admin1@example.com",
                Phone = "123456789",
                Address = "address",
                Username = "admin1",
                Password = "password"
            };

            var mockAdminsDbSet = new Mock<DbSet<Admin>>();
            var adminData = new List<Admin> { admin }.AsQueryable();

            mockAdminsDbSet.As<IQueryable<Admin>>().Setup(m => m.Provider).Returns(adminData.Provider);
            mockAdminsDbSet.As<IQueryable<Admin>>().Setup(m => m.Expression).Returns(adminData.Expression);
            mockAdminsDbSet.As<IQueryable<Admin>>().Setup(m => m.ElementType).Returns(adminData.ElementType);
            mockAdminsDbSet.As<IQueryable<Admin>>().Setup(m => m.GetEnumerator()).Returns(adminData.GetEnumerator());

            _mockContext.Setup(c => c.Admins).Returns(mockAdminsDbSet.Object);

            // Act
            _adminRepository.RemoveAdmin(admin);

            // Assert
            mockAdminsDbSet.Verify(m => m.Remove(It.IsAny<Admin>()), Times.Once());
            _mockContext.Verify(c => c.SaveChanges(), Times.Never());
        }

        [Test]
        public void SaveChanges_SavesToDatabase()
        {
            // Act
            _adminRepository.SaveChanges();

            // Assert
            _mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }
    }
}
