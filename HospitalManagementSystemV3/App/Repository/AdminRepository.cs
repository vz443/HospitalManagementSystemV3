using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagementSystemV3.App.Repository
{
    class AdminRepository : Repository<Admin>
    {
        public AdminRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        AppDbContext _context;

        public void Add(Admin entity)
        {
            _context.Admins.Add(entity);
        }

        public IEnumerable<Admin> Find(Expression<Func<Admin, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients;
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors;
        }

        public Patient GetPatientById(string id)
        {
            return _context.Patients
                           .Include(p => p.Doctor)
                           .Include(p => p.Appointments)
                           .ThenInclude(a => a.Doctor)
                           .FirstOrDefault(p => p.Username == id);
        }

        public Doctor GetDoctorById(string id)
        {
            return _context.Doctors
                            .Include(d => d.Patients)
                            .FirstOrDefault(d => d.Username == id);
        }

        public void AddDoctor(Doctor entity)
        {

        }

        public void AddPatient(Patient entity)
        {

        }

        public void RemoveAdmin(Admin entity)
        {
            _context.Admins.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Admin entity)
        {
            throw new NotImplementedException();
        }
    }
}
