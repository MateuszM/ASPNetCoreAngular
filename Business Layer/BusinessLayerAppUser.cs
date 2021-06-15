using InfrastructureLayer.Data;
using InfrastructureLayer.Model;
using System;
using System.Threading.Tasks;

namespace Business_Layer
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
