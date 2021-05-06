﻿using InfrastructureLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreAngularShop.Controllers
{
    public class AccountGuestController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountGuestController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Post([FromBody] SignInModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser appUser = new AppUser{ UserName=model.Email,FirstName=model.FirstName,LastName=model.LastName,Email=model.Email};
            var result = await _userManager.CreateAsync(appUser);
            if(result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
        private async Task AddUserClaims(AppUser user,SignInModel model)
        {
            await _userManager.AddClaimsAsync(user, new List<Claim>() {new Claim("userName",user.UserName),
                                                                       new Claim("email",user.Email),
                                                                       new Claim("role",model.Role
                                                                       )}.ToList());
        }
        private async Task AddUserClaims(AppUser user, IEnumerable<Claim> claims)
        {
            await _userManager.AddClaimsAsync(user, claims);
        }
    }
}