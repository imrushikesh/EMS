using EMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TblEmployee> Employees { get; set; }
        public DbSet<TblUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUser>().HasIndex(u => u.UserId).IsUnique();
            modelBuilder.Entity<TblEmployee>().HasIndex(u => u.EmpId).IsUnique();
        }

    }
}
