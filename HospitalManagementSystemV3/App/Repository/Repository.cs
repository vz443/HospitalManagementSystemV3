using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.App.Interface;

namespace LibraryApp.Repositories
{
    // Generic repository implementation
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context; // Database context instance
        private readonly DbSet<T> _dbSet; // DbSet instance for the generic type

        public Repository(AppDbContext context)
        {
            _context = context; // Injected database context
            _dbSet = _context.Set<T>(); // Initialize the DbSet for the generic type
        }

        public T GetById(string id)
        {
            return _dbSet.Find(id); // Find entity by ID
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList(); // Retrieve all entities
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList(); // Find entities by predicate
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity); // Add entity to DbSet
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity); // Update entity in DbSet
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity); // Remove entity from DbSet
        }

        public void SaveChanges()
        {
            _context.SaveChanges(); // Save changes to the database
        }
    }
}
