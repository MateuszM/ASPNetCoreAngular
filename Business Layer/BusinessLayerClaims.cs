using InfrastructureLayer.Data;
using InfrastructureLayer.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
   public class BusinessLayerClaims
    {
        private AppIdentityDbContext context;
        public BusinessLayerClaims(AppIdentityDbContext dbContext)
        {
            context = dbContext;
        }
        public List<IdentityUserClaim<string> >LoadUserClaims(AppUser user)
        {
            return context.UserClaims.Where(x => x.UserId == user.Id).ToList();
        }
    }
}
