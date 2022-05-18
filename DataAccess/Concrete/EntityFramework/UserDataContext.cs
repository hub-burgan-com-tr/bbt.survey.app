using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserDataContext : DbContext
    {
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(@"Host=localhost;Database=BurganUser;Username=postgres;Password=lamal159***");
            //optionsBuilder.UseNpgsql(@"Host=18.192.189.47:5433;Database=yugabyte;Username=admin;Password=rd2OZvpjbrthkDc0ASgiteXnUKCX6Y");
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-0UBP1P5;Database=SurveyTest;Trusted_Connection=true");
            //optionsBuilder.UseSqlServer(@"Server=10.180.20.125;Database=SurveyTest;User Id=DEBUGUSER;Password=aAR=GsG4");
            //optionsBuilder.UseSqlServer(@"Server=10.200.0.14;Database=HUMANIST;User Id=DEBUGUSER;Password=aAR=GsG4");
            //optionsBuilder.UseSqlServer(@"Server=10.200.0.14;Database=HUMANIST;Trusted_Connection=true");
            //optionsBuilder.UseSqlServer(@"Server=10.180.20.125;Database=SurveyTest;User Id=DEBUGUSER;Password=aAR=GsG4");

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{GetEnviroment()}.json", false, true)

           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDataConnection"));


            base.OnConfiguring(optionsBuilder);

        }
        string? GetEnviroment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public DbSet<User>? PERTRANS1 { get; set; }
    }
}
