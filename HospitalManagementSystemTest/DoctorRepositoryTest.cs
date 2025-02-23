﻿
using HospitalManagementSystemV3.App.Repository;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagementSystemTest
{
    class DoctorRepositoryTest
    {
        private Mock<AppDbContext>? _mockContext;
        private DoctorRepository? _doctorRepository;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<AppDbContext>();

            // Mock DbSet properties
            var mockPatients = new Mock<DbSet<Patient>>();
            var mockAppointments = new Mock<DbSet<Appointment>>();
            var mockDoctors = new Mock<DbSet<Doctor>>();

            _mockContext.Setup(c => c.Patients).Returns(mockPatients.Object);
            _mockContext.Setup(c => c.Appointments).Returns(mockAppointments.Object);
            _mockContext.Setup(c => c.Doctors).Returns(mockDoctors.Object);

            // Create repository instance
            _doctorRepository = new DoctorRepository(_mockContext.Object);
        }

        [Test]
        public void AddDoctor_AddsDoctor()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = "Dr. Smith",
                Email = "smith@example.com",
                Phone = "123456",
                Address = "123 Main St",
                Username = "drsmith",
                Password = "password"
            };

            var mockDoctorsDbSet = new Mock<DbSet<Doctor>>();
            var doctorData = new List<Doctor>().AsQueryable();

            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(doctorData.Provider);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(doctorData.Expression);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(doctorData.ElementType);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(doctorData.GetEnumerator());

            _mockContext.Setup(c => c.Doctors).Returns(mockDoctorsDbSet.Object);

            // Act
            _doctorRepository.AddDoctor(doctor);

            // Assert
            mockDoctorsDbSet.Verify(m => m.Add(It.IsAny<Doctor>()), Times.Once());
            _mockContext.Verify(c => c.SaveChanges(), Times.Never());
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
            var result = _doctorRepository.GetAllPatients();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAllAppointmentsForDoctor_ReturnsAppointments()
        {
            // Arrange
            var doctor = new Doctor { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", Phone = "123456", Address = "123 Main St", Username = "john", Password = "password" };
            var appointments = new List<Appointment>
            {
                new Appointment { Id = Guid.NewGuid(), PatientId = Guid.NewGuid(), DoctorId = doctor.Id, Description = "Check-up"},
                new Appointment { Id = Guid.NewGuid(), PatientId = Guid.NewGuid(), DoctorId = doctor.Id, Description = "Follow-up"}
            }.AsQueryable();

            var mockAppointmentsDbSet = new Mock<DbSet<Appointment>>();
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(appointments.Provider);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(appointments.Expression);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(appointments.ElementType);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(appointments.GetEnumerator());

            _mockContext.Setup(c => c.Appointments).Returns(mockAppointmentsDbSet.Object);

            // Act
            var result = _doctorRepository.GetAllAppointmentsForDoctor(doctor);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(a => a.DoctorId == doctor.Id));
        }

        [Test]
        public void GetPatientById_ReturnsPatient()
        {
            // Arrange
            var doctor = new Doctor { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", Phone = "123456", Address = "123 Main St", Username = "john", Password = "password" };
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john@example.com",
                Phone = "123456",
                Address = "123 Main St",
                Username = "john",
                Password = "password",
                Doctor = doctor,
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
            var result = _doctorRepository.GetPatientById(patient.Username);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(patient.Username, result.Username);
        }

        [Test]
        public void SaveChanges_SavesToDatabase()
        {
            // Act
            _doctorRepository.SaveChanges();

            // Assert
            _mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateDoctor_UpdatesDoctor()
        {
            // Arrange
            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = "Dr. Smith",
                Email = "smith@example.com",
                Phone = "123456",
                Address = "123 Main St",
                Username = "drsmith",
                Password = "password"
            };

            var mockDoctorsDbSet = new Mock<DbSet<Doctor>>();
            var doctorData = new List<Doctor> { doctor }.AsQueryable();

            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(doctorData.Provider);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(doctorData.Expression);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(doctorData.ElementType);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(doctorData.GetEnumerator());

            _mockContext.Setup(c => c.Doctors).Returns(mockDoctorsDbSet.Object);

            // Act
            doctor.Name = "Dr. Updated";
            _doctorRepository.Update(doctor);

            // Assert
            mockDoctorsDbSet.Verify(m => m.Update(It.IsAny<Doctor>()), Times.Once());
            _mockContext.Verify(c => c.SaveChanges(), Times.Never());
        }
    }
}
