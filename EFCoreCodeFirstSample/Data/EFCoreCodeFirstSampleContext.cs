using EFCoreCodeFirstSample.Configurations.Entities;
using EFCoreCodeFirstSample.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreCodeFirstSample.Data
{
    public class EFCoreCodeFirstSampleContext : IdentityDbContext<ApiUser>
    {
        private readonly IConfiguration _configuration;
        public EFCoreCodeFirstSampleContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("EmployeeAppCon"));
        }

        public DbSet<Employee> Employees { get; set; } = null;
        public DbSet<Department> Departments { get; set; } = null;
        //public DbSet<UserDTO> Users { get; set; } = null;
        public DbSet<Country> Countries { get; set; } = null;
        public DbSet<Hotel> Hotels { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());

            base.OnModelCreating(builder);
        }

    }
}
