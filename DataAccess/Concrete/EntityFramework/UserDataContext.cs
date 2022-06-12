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
            

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)

           .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
           
           

           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDataConnection"));


            base.OnConfiguring(optionsBuilder);

        }
        
        
        

        public DbSet<User>? PERTRANS1 { get; set; }
    }
}
