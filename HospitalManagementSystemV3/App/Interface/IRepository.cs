using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.App.Interface
{
    interface IRepository<T> where T : class
    {
        T GetById(string userID); // Get entity by ID
        IEnumerable<T> GetAll(); // Get all entities
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate); // Find entities by predicate
        void Add(T entity); // Add entity
        void Update(T entity); // Update entity
        void Remove(T entity); // Remove entity
        void SaveChanges(); // Save changes to database
    }
}
