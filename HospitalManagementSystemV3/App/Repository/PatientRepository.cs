using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalManagementSystemV3.App.Repository
{
    public class PatientRepository : Repository<Patient>
    {
        public PatientRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors.ToList(); // Fetch all doctors from the database
        }

        public void UpdatePatient(Patient patient)
        {
            _context.Patients.Update(patient); // Update the patient entity in the context
        }

        public void AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment); // Add the appointment entity to the context
        }

        public IEnumerable<Appointment> GetAllAppointmentsForPatient(Patient patient)
        {
            return _context.Appointments
                           .Include(a => a.Doctor)
                           .Where(a => a.PatientId == patient.Id)
                           .ToList(); // Fetch all appointments for a specific patient
        }
        public new void SaveChanges()
        {
            _context.SaveChanges(); // Save changes to the database
        }
    }
}
