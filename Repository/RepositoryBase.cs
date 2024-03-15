using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // Generic repository
    // All methods are kept as synchronous
    // That’s because we want to leave a possibility for the repository user classes to have either sync or async method execution.
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        public IQueryable<T> FindAll(bool trackChanges)
        {
            var result = trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
            return result;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            var result = trackChanges ? _context.Set<T>().Where(expression) : _context.Set<T>().Where(expression).AsNoTracking();
            return result;
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
