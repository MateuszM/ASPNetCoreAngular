using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetOverview(Expression<Func<T, bool>> predicate = null);
        T GetDetail(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetIncludes(params Expression<Func<T, Object>>[] includes);
        Task<IEnumerable<T>> GetIncludesAsync(params Expression<Func<T, Object>>[] includes);

    }
}