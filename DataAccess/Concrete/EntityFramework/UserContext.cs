using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework
{
    //Context database tabloları ile proje classlarına bağlamak için vardır.
    public class UserContext : DbContext
    {



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.Prod.json",optional:false,reloadOnChange:true)
            

            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserConnection"));


            base.OnConfiguring(optionsBuilder);

        }
        
        
       


        public DbSet<UserTest>? UserTest { get; set; }
        public DbSet<VoteLimit>? VoteLimits { get; set; }
        public DbSet<UserInfo>? Users { get; set; }
        public DbSet<Vote>? Votes { get; set; }




    }
}
