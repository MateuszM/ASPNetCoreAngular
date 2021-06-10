using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data
{
    public interface IUserRepository<T>
    {
        Task InsertEntity(string role, string id, string fullName);
        Task InsertUser(T user);

    }
}
