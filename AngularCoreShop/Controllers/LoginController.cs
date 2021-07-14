﻿using IdentityServer4.Events;
using IdentityServer4.Services;
using Infrastructure.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AngularCoreShop.Models;
using AngularCoreShop.Models.ViewModels;
using ServiceLayer;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AngularCoreShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEventService events;
        private readonly IIdentityServerInteractionService interaction;
        private IAccountService accountService;
        public IActionResult Index()
        {
            return View();
        }
        public LoginController(UserManager<AppUser> userManager, IEventService events, IIdentityServerInteractionService interaction, IAccountService service)
        {
            this.userManager = userManager;
            this.events = events;
            this.interaction = interaction;
            this.accountService = service;
        }
        public async Task<IActionResult> Login(LoginInputViewModel model)
        {
            var context = interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    await events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.FirstName, user.LastName));
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };
                    ClaimsPrincipal principal = accountService.CreatePrincipal(user);
                    if (principal == null)
                    {
                        throw new Exception("invalid return URL");
                    }
                    await HttpContext.SignInAsync(principal, props);

                    if (context != null)
                    {
                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Redirect(model.ReturnUrl);
                    }
                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        // user might have clicked on a malicious link - should be logged
                        throw new Exception("invalid return URL");
                    }
                }
                await events.RaiseAsync(new UserLoginFailureEvent(model.UserName, "invalid credentials"));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            var vm = new LoginInputViewModel() { UserName = model.UserName, RememberLogin = model.RememberLogin };
            return View(vm);
        }




    }
}
