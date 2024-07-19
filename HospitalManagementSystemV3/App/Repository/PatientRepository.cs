using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using System.Linq.Expressions;

namespace HospitalManagementSystemV3.App.Repository
{
    class PatientRepository : Repository<Patient>
    {
        PatientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        AppDbContext _context;

        public void Add(Patient entity)
        {
            _context.Patients.Add(entity);
        }

        public IEnumerable<Patient> Find(Expression<Func<Patient, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients;
        }

        public Patient GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Patient entity)
        {
            _context.Patients.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
