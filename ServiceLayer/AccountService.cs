using System;
using System.Collections.Generic;
using System.Security.Claims;
using BusinessLayer;
using Infrastructure.Data;
using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ServiceLayer
{
    public class AccountService : IAccountService
    {
        private AppIdentityDbContext context;
        private IClaimsLogic claims;
        public AccountService(AppIdentityDbContext dbContext)
        {
            this.context = dbContext;
            claims = new ClaimsLogic(dbContext);
        }
        public void SetBusinessLayerClaims(IClaimsLogic claims)
        {
            this.claims = claims;
        }
        public ClaimsPrincipal CreatePrincipal(AppUser user)
        {
            
            List<Claim> ListClaims = ConvertFromIdentityToClaims(claims.LoadUserClaims(user));
            Claim Role = GetRole(ListClaims);
            if (Role.Value == "Customer")
            {
                var UserIdentity = new ClaimsIdentity(ListClaims, "Customer");
                return new ClaimsPrincipal(UserIdentity);
            }
            if (Role.Value == "Employee")
            {
                //TODO
            }
            if (Role.Value == "SuperAdmin")
            {
                //TODO
            }
            return null;

        }

        private Claim GetRole(List<Claim> converted)
        {
            return converted.Find(x => x.Type == "Role");
        }
        private List<Claim> ConvertFromIdentityToClaims(List<IdentityUserClaim<string>> identityUserClaimsList)
        {
            List<Claim> temp = new List<Claim>();

            foreach (IdentityUserClaim<string> s in identityUserClaimsList)
            {
                temp.Add(s.ToClaim());
            }
            return temp;
        }
    }
}
