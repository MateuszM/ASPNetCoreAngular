using Infrastructure.Data;
using Infrastructure.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLayer;
using System.Reflection;

namespace AngularCoreShop
{
    public class Startup
    {
        private const string _connectionStringName = "DefaultConnection";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        protected virtual void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(ConfigureDatabase);
        }

        protected virtual void ConfigureDatabase(DbContextOptionsBuilder ctxBuilder)
        {
            ctxBuilder.UseSqlServer(Configuration.GetConnectionString(_connectionStringName));
        }
        protected string GetConnectionString()
        {
            return Configuration.GetConnectionString(_connectionStringName);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            AddDbContext(services);
            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddControllersWithViews();
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
            }
            ).AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(GetConnectionString(),
                    sql => sql.MigrationsAssembly("InfrastructureLayer"));
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 60;

            }).AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(GetConnectionString(),
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            }).
            AddInMemoryIdentityResources(Config.GetIdentityResources()).
            AddInMemoryApiResources(Config.GetApiResources()).
            AddInMemoryClients(Config.Clients()).
            AddAspNetIdentity<AppUser>();
            //services.AddDbContext<AppIdentityDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddTransient<IAccountService>(s => new AccountService(s.GetService<AppIdentityDbContext>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseIdentityServer();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
