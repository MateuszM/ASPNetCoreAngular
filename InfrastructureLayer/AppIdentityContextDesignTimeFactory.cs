using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public class AppIdentityContextDesignTimeFactory : IDesignTimeDbContextFactory<AppIdentityDBContext>
    {

        public AppIdentityDBContext CreateDbContext(string[] args)
        {
            var path = @"C:\Users\Bezimienny\source\repos\NetCoreAngularShop\InfrastructureLayer\bin\Debug\net5.0";
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
         IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{envName}.json", optional: false)
                .Build();
            var builder = new DbContextOptionsBuilder<AppIdentityDBContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Tut");
            Console.WriteLine(AppContext.BaseDirectory);
            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine(Directory.GetCurrentDirectory());



            builder.UseSqlServer(connectionString);
            return new AppIdentityDBContext(builder.Options);
        }
    }
}

