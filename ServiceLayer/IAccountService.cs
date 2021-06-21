using InfrastructureLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface IAccountService
    {
        public ClaimsPrincipal CreatePrincipal(AppUser user);
    }
}
