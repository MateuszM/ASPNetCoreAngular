using System;
using System.Collections.Generic;
using System.Security.Claims;
using Business_Layer;
using InfrastructureLayer.Data;
using InfrastructureLayer.Model;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer
{
    public class AccountService : IAccountService
    {
        private AppIdentityDbContext context;
        public AccountService (AppIdentityDbContext dbContext)
        {
            this.context = dbContext;
        }
        public ClaimsPrincipal GetPrincipal(AppUser user)
        {
            BusinessLayerClaims claims = new BusinessLayerClaims(context);
            var UserIdentity = new ClaimsIdentity(ConvertFromIdentityToClaims(claims.LoadUserClaims(user)), "Customer");
            var ClaimsPrincipal = new ClaimsPrincipal(UserIdentity);
            return ClaimsPrincipal;
        }
        private List<Claim> ConvertFromIdentityToClaims(List<IdentityUserClaim<string>> identityUserClaimsList)
        {
            List<Claim> temp = new List<Claim>();

            foreach(IdentityUserClaim<string> s in identityUserClaimsList)
            {
                temp.Add(s.ToClaim());
            }
            return temp;
        }
    }
}
