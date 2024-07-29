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
        private Mock<DbSet<T>> CreateMockDbSet<T>(IEnumerable<T> elements) where T : class
        {
            var queryable = elements.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSet;
        }

        [Test]
        public void GetAllDoctors_ReturnsAllDoctors()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. Alice", Email = "alice@example.com", Phone = "123-456-7890", Address = "123 Main St, Cityville", Username = "alice123", Password = "securepassword1" },
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. Bob", Email = "bob@example.com", Phone = "234-567-8901", Address = "456 Elm St, Townsville", Username = "bob234", Password = "securepassword2" },
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. Carol", Email = "carol@example.com", Phone = "345-678-9012", Address = "789 Pine St, Villagetown", Username = "carol345", Password = "securepassword3" },
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. Dave", Email = "dave@example.com", Phone = "456-789-0123", Address = "101 Maple St, Hamlet", Username = "dave456", Password = "securepassword4" },
                new Doctor { Id = Guid.NewGuid(), Name = "Dr. Eve", Email = "eve@example.com", Phone = "567-890-1234", Address = "202 Oak St, Metropolis", Username = "eve567", Password = "securepassword5" }
            };
            var mockDbSet = CreateMockDbSet(doctors);
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Doctors).Returns(mockDbSet.Object);
            var repository = new PatientRepository(mockContext.Object);

            // Act
            var result = repository.GetAllDoctors();

            // Assert
            Assert.Equals(2, result.Count());
            Assert.Equals("Dr. A", result.First().Name);
        }

        [Test]
        public void AddAppointment_AddsAppointment()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Appointment>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Appointments).Returns(mockDbSet.Object);
            var repository = new PatientRepository(mockContext.Object);
            var appointment = new Appointment { Id = 234, PatientId = Guid.NewGuid(), DoctorId = Guid.NewGuid(), Description = "Test Appointment" };

            // Act
            repository.AddAppointment(appointment);
            repository.SaveChanges();

            // Assert
            mockDbSet.Verify(m => m.Add(It.IsAny<Appointment>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdatePatient_UpdatesPatient()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Patient>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockDbSet.Object);
            var repository = new PatientRepository(mockContext.Object);
            var patient = new Patient { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com", Phone = "123-456-7890", Address = "456 Maple St, Townsville", Username = "johndoe", Password = "securepassword" };
            // Act
            repository.UpdatePatient(patient);
            repository.SaveChanges();

            // Assert
            mockDbSet.Verify(m => m.Update(It.IsAny<Patient>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void GetAllAppointmentsForPatient_ReturnsAppointments()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patient = new Patient { Id = patientId, Name = "John Doe", Email = "john.doe@example.com", Phone = "123-456-7890", Address = "456 Maple St, Townsville", Username = "johndoe", Password = "securepassword" };

            var doctor1 = new Doctor { Id = Guid.NewGuid(), Name = "Dr. Smith", Email = "dr.smith@example.com", Phone = "111-222-3333", Address = "123 Elm St, Townsville", Username = "username", Password = "password" };
            var doctor2 = new Doctor { Id = Guid.NewGuid(), Name = "Dr. Johnson", Email = "dr.johnson@example.com", Phone = "444-555-6666", Address = "789 Pine St, Townsville", Username = "username", Password = "password"};

            var appointments = new List<Appointment>
            {
                new Appointment { Id = 222, PatientId = patientId, DoctorId = doctor1.Id, Doctor = doctor1, Description = "Checkup" },
                new Appointment { Id = 223, PatientId = patientId, DoctorId = doctor2.Id, Doctor = doctor2, Description = "Follow-up" }
            };

            var mockAppointmentDbSet = CreateMockDbSet(appointments);
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Appointments).Returns(mockAppointmentDbSet.Object);

            var repository = new PatientRepository(mockContext.Object);

            // Act
            var result = repository.GetAllAppointmentsForPatient(patient);

            // Assert
            Assert.Equals(2, result.Count());
        }
    }
}
