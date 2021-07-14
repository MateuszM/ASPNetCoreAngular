using Infrastructure.Data;
using Infrastructure.Model;
using System;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BusinessLayerAppUser
    {
        private AppIdentityDbContext dbContext;
        public BusinessLayerAppUser(AppIdentityDbContext context)
        {
            dbContext = context;
        }
        public void SaveUser(AppUser user)
        {
            dbContext.Add(user);
            dbContext.SaveChanges();
        }
    }
}
