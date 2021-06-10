using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data
{
    class UserRepository<T> : IUserRepository<T> where T: class
    {
        private AppIdentityDbContext db;
        public UserRepository(AppIdentityDbContext context)
        {
            db = context;
        }
        public Task InsertEntity(string role, string id, string fullName)
        {
            throw new NotImplementedException();
        }

        public Task void InsertUser(T user)
        {
            db.Set<T>().Add(user);
        }
    }
}
