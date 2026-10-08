using Microsoft.EntityFrameworkCore;
using NuxibaApi.Models;

namespace NuxibaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("ccUsers");
            modelBuilder.Entity<Area>().ToTable("ccRIACat_Areas");
            modelBuilder.Entity<Login>().ToTable("ccloglogin");
        }
    }
}