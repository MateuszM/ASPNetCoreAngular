using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCoreShop.Models
{
    public class SignInResponseModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public SignInResponseModel()
        { }
        public SignInResponseModel(AppUser user, string role)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Id = user.Id;
            Role = role;

        }

    }

}
