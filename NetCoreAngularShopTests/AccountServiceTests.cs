using Business_Layer;
using InfrastructureLayer.Data;
using InfrastructureLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Tests
{
    [TestClass()]
    public class AccountServiceTests
    {
        AppUser userEmployee = new AppUser() { UserName = "SimpleName", Email = "Some@Mail.com", LastName = "NowWis", FirstName = "WisNow" };
        AppUser userCustomer = new AppUser() { UserName = "SimpleName", Email = "Some@Mail.com", LastName = "NowWis", FirstName = "WisNow" };
        public class FakeClaimLogicClass : IClaimsLogic
        {
            public List<IdentityUserClaim<string>> LoadUserClaims(AppUser user)
            {
                return new List<IdentityUserClaim<string>>() { new IdentityUserClaim<string>() { UserId = "1", ClaimType = "Role", ClaimValue = "Employee", Id = 1 } };
            }
        }
        public class FakeClaimLogicTrueClass : IClaimsLogic
        {
            public List<IdentityUserClaim<string>> LoadUserClaims(AppUser user)
            {
                return new List<IdentityUserClaim<string>>() { new IdentityUserClaim<string>() { UserId = "1", ClaimType = "Role", ClaimValue = "Customer", Id = 1 } };
            }
        }
        [TestMethod()]
        public void AccountServiceTest()
        {
            AccountService accountService = new AccountService(new AppIdentityDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<AppIdentityDbContext>()));
            accountService.SetBusinessLayerClaims(new FakeClaimLogicClass());
            Assert.IsNull(accountService.CreatePrincipal(userEmployee));
        }
        [TestMethod()]
        public void CreatePrincipalForCustomerTest()
        {
            AccountService accountService = new AccountService(new AppIdentityDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<AppIdentityDbContext>()));
            accountService.SetBusinessLayerClaims(new FakeClaimLogicTrueClass());
            Assert.IsTrue(accountService.CreatePrincipal(userCustomer).FindFirst("Role").Value == "Customer");
        }


        [TestMethod()]
        public void CreatePrincipalTest()
        {
            Assert.Fail();
        }
    }
}