using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using HospitalManagementSystemV3.App.Repository;
using System.Numerics;
namespace HospitalManagementSystemTest
{
    public class PatientRepositoryTest
    {
        private Mock<AppDbContext>? _mockContext;
        private PatientRepository? _patientRepository;

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
            _patientRepository = new PatientRepository(_mockContext.Object);
        }

        [Test]
        public void TestGetAllDoctors_Success()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. A", Email = "a@example.com", Phone = "123456", Address = "Address A", Username = "dr.a", Password = "passA" },
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. B", Email = "b@example.com", Phone = "234567", Address = "Address B", Username = "dr.b", Password = "passB" }
            }.AsQueryable();

            var mockDoctorsDbSet = new Mock<DbSet<Doctor>>();
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(doctors.Provider);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(doctors.Expression);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(doctors.ElementType);
            mockDoctorsDbSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(doctors.GetEnumerator());

            _mockContext.Setup(c => c.Doctors).Returns(mockDoctorsDbSet.Object);

            // Act
            var result = _patientRepository.GetAllDoctors();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void AddAppointment_AddsAppointment()
        {
            // Arrange
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                DoctorId = Guid.NewGuid(),
                Description = "Check-up"
            };

            var mockAppointmentsDbSet = new Mock<DbSet<Appointment>>();
            var appointmentData = new List<Appointment>().AsQueryable();

            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(appointmentData.Provider);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(appointmentData.Expression);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(appointmentData.ElementType);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(appointmentData.GetEnumerator());

            _mockContext.Setup(c => c.Appointments).Returns(mockAppointmentsDbSet.Object);

            // Act
            _patientRepository.AddAppointment(appointment);

            // Assert
            mockAppointmentsDbSet.Verify(m => m.Add(It.IsAny<Appointment>()), Times.Once());
            _mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }



        [Test]
        public void UpdatePatient_UpdatesPatient()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patient = new Patient
            {
                Id = patientId,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123456",
                Address = "123 Main St",
                Username = "johndoe",
                Password = "password"
            };

            var mockPatientsDbSet = new Mock<DbSet<Patient>>();
            var patientData = new List<Patient> { patient }.AsQueryable();

            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patientData.Provider);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patientData.Expression);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patientData.ElementType);
            mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patientData.GetEnumerator());

            _mockContext.Setup(c => c.Patients).Returns(mockPatientsDbSet.Object);
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            // Act
            patient.Name = "John Updated";
            _patientRepository.UpdatePatient(patient);

            // Assert
            mockPatientsDbSet.Verify(m => m.Update(It.IsAny<Patient>()), Times.Once());
            _mockContext.Verify(c => c.SaveChanges(), Times.Once());
        }


        [Test]
        public void GetAllAppointmentsForPatient_ReturnsAppointments()
        {
            // Arrange
            var patient = new Patient { Id = Guid.NewGuid(), Name = "Dr. B", Email = "b@example.com", Phone = "234567", Address = "Address B", Username = "dr.b", Password = "passB" };
            var appointments = new List<Appointment>
            {
                new Appointment { Id = Guid.NewGuid(), PatientId = patient.Id, DoctorId = Guid.NewGuid(), Description = "Check-up"},
                new Appointment { Id = Guid.NewGuid(), PatientId = patient.Id, DoctorId = Guid.NewGuid(), Description = "Follow-up"}
            }.AsQueryable();

            var mockAppointmentsDbSet = new Mock<DbSet<Appointment>>();
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.Provider).Returns(appointments.Provider);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.Expression).Returns(appointments.Expression);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.ElementType).Returns(appointments.ElementType);
            mockAppointmentsDbSet.As<IQueryable<Appointment>>().Setup(m => m.GetEnumerator()).Returns(appointments.GetEnumerator());

            _mockContext.Setup(c => c.Appointments).Returns(mockAppointmentsDbSet.Object);

            // Act
            var result = _patientRepository.GetAllAppointmentsForPatient(patient);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(a => a.PatientId == patient.Id));
        }

    }
}
