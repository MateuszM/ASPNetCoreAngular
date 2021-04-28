using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {

        private AppIdentityDBContext _context = null;
        DbSet<T> _objectSet;
        public GenericRepository(AppIdentityDBContext _db)
        {
            _context = _db;
            _objectSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }
        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _objectSet.ToListAsync();
        }
        public IEnumerable<T> GetIncludes(params Expression<Func<T, Object>>[] includes)
        {
            IQueryable<T> query = _objectSet.Include(includes[0]);
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }
            return query.ToList();
        }
        public async Task<IEnumerable<T>> GetIncludesAsync(params Expression<Func<T, Object>>[] includes)
        {
            IQueryable<T> query = _objectSet.Include(includes[0]);
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _objectSet.ToList<T>();
        }

        public T GetDetail(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.First(predicate);
        }

        public IEnumerable<T> GetOverview(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return _objectSet.Where(predicate);
            return _objectSet.AsEnumerable();
        }

    }
}