using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AngularCoreShop;

namespace AngularCoreShopTests
{
    public class StartUpTest : Startup
    {
        public StartUpTest(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void AddDbContext(IServiceCollection services)
        {
            base.AddDbContext(services);
           // services.Add<AppIdentityDbContext>();
        }

        protected override void ConfigureDatabase(DbContextOptionsBuilder ctxBuilder)
        {
         //  ctxBuilder.UseInMemoryDatabase();
        }
    }
}

