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
    class PatientRepository : Repository<Patient>
    {
        public PatientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;

        public void Add(Patient entity)
        {
            _context.Patients.Add(entity);
        }

        public IEnumerable<Patient> Find(Expression<Func<Patient, bool>> predicate)
        {
            return _context.Patients.Where(predicate).ToList();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.Include(p => p.Doctor).ToList();
        }

        public Patient GetById(string userID)
        {
            return _context.Patients.Include(p => p.Doctor).FirstOrDefault(p => p.Username == userID);
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
            _context.Patients.Update(entity);
        }

        public void GetPatientAppointments(Patient entity)
        {

        }
    }
}
