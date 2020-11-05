using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AuthAPI.Models;

namespace AuthAPI.Data
{
    public class AuthContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public AuthContext(IConfiguration configuration,DbContextOptions<DbContext> options):base(options){
            _configuration=configuration;
        }
        public DbSet<User> Users{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<User>().HasKey(k=>k.ID);
        }
    }
}