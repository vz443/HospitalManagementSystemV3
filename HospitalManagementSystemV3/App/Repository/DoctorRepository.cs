using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementSystemV3.App.Repository
{
    class DoctorRepository : Repository<Doctor>
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }

        public void AddDoctor(Doctor entity)
        {
            _context.Doctors.Add(entity); // Add a new doctor to the context
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.ToList(); // Retrieve all patients from the database
        }

        public Patient GetPatientById(string id)
        {
            return _context.Patients
                           .Include(p => p.Doctor)
                           .Include(p => p.Appointments)
                           .FirstOrDefault(p => p.Username == id); // Find the patient by ID and include related entities
        }

        public new void SaveChanges()
        {
            _context.SaveChanges(); // Save changes to the database
        }

        public void Update(Doctor entity)
        {
            _context.Doctors.Update(entity); // Update the doctor entity in the context
        }
    }
}
