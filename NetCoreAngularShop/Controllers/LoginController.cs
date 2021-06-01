using IdentityServer4.Events;
using IdentityServer4.Services;
using InfrastructureLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAngularShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEventService events;
        private readonly IIdentityServerInteractionService interaction;
        public IActionResult Index()
        {
            return View();
        }
        public LoginController(UserManager<AppUser> userManager,IEventService events, IIdentityServerInteractionService interaction)
        {
            this.userManager = userManager;
            this.events = events;
            this.interaction = interaction;
        }
        public async Task<IActionResult> Login(LoginInputViewModel model)
        {
            var context = interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
               
                if(user!=null && await userManager.CheckPasswordAsync(user,model.Password))
                {
                   await events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.FirstName, user.LastName));
                }
            }
            var vm = new LoginInputViewModel() { UserName = model.UserName, RememberLogin = model.RememberLogin };
            return View(vm);
        }
    }
}
