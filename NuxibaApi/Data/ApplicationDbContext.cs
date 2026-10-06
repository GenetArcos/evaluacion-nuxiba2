using Microsoft.EntityFrameworkCore;
using NuxibaApi.Models;

namespace NuxibaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Area> Areas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LogLogin> LogLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().ToTable("ccRIACat_Areas").HasKey(a => a.IdArea);
            modelBuilder.Entity<User>().ToTable("ccUsers").HasKey(u => u.IdUser);
            modelBuilder.Entity<LogLogin>().ToTable("ccloglogin").HasKey(l => l.IdLog);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Area)
                .WithMany()
                .HasForeignKey(u => u.IdArea);

            modelBuilder.Entity<LogLogin>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.IdUser);
        }
    }
}