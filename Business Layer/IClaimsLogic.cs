using InfrastructureLayer.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public interface IClaimsLogic
    {
        public List<IdentityUserClaim<string>> LoadUserClaims(AppUser user);
    }
}
