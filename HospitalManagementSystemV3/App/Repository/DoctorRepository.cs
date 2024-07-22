using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using LibraryApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HospitalManagementSystemV3.App.Repository
{
    class DoctorRepository : Repository<Doctor>
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;

        public void Add(Doctor entity)
        {
            _context.Doctors.Add(entity);
        }

        public IEnumerable<Doctor> Find(Expression<Func<Doctor, bool>> predicate)
        {
            return _context.Doctors.Where(predicate).ToList();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.Include(d => d.Patients).Include(d => d.Appointments).ToList();
        }

        public Doctor GetByuserId(string username)
        {
            return _context.Doctors.Include(d => d.Patients)
                                       .Include(d => d.Appointments)
                                       .FirstOrDefault(d => d.Username == username);
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
            _context.Doctors.Update(entity);
        }
    }
}
