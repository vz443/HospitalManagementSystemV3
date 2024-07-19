using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using System.Linq.Expressions;

namespace HospitalManagementSystemV3.App.Repository
{
    class DoctorRepository : Repository<Doctor>
    {
        DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        AppDbContext _context;

        public void Add(Doctor entity)
        {
            _context.Doctors.Add(entity);
        }

        public IEnumerable<Doctor> Find(Expression<Func<Doctor, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors;
        }

        public Doctor GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Doctor entity)
        {
            _context.Doctors.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
