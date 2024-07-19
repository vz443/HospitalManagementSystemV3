using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using System.Linq.Expressions;

namespace HospitalManagementSystemV3.App.Repository
{
    class AdminRepository : Repository<Admin>
    {
        AdminRepository(AppDbContext context) : base(context)
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

        public IEnumerable<Admin> GetAll()
        {
            return _context.Admins;
        }

        public Admin GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Admin entity)
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
