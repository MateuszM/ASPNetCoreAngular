using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public interface IUnitOfWork
    {

        IRepository<T> Repository<T>() where T : class;

        Task<int> Commit();

        void Rollback();
    }
}
