using Infrastructure.Data;
using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class ClaimsLogic : IClaimsLogic
    {
        private AppIdentityDbContext context;
        public ClaimsLogic(AppIdentityDbContext dbContext)
        {
            context = dbContext;
        }
        public List<IdentityUserClaim<string> >LoadUserClaims(AppUser user)
        {
            return context.UserClaims.Where(x => x.UserId == user.Id).ToList();
        }
    }
}