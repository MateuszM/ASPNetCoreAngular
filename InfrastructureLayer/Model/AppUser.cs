using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Model
{
   public class AppUser : IdentityUser
    {
        public string firstName;
        public string lastName;
    }
}
