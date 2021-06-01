using InfrastructureLayer.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreAngularShop;

namespace NetCoreAngularShopTests
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

