using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVPApp.Models;

namespace MVPApp.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public virtual DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string environment = _configuration["Environment"];

            if (environment == "Testing")
            {
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
            }
            else
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}