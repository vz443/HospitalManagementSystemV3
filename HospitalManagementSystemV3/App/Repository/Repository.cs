using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories
{
    // Generic repository implementation
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context; // Database context instance

        public Repository(AppDbContext context)
        {
            _context = context; // Injected database context
        }

        public T GetById(string id)
        {
            return _context.Set<T>().Find(id); // Find entity by ID
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList(); // Retrieve all entities
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList(); // Find entities by predicate
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity); // Add entity to DbSet
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity); // Update entity in DbSet
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity); // Remove entity from DbSet
        }

        public void SaveChanges()
        {
            _context.SaveChanges(); // Save changes to the database
        }
    }
}
