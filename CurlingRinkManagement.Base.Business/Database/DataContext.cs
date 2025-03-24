using CurlingRinkManagement.Base.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CurlingRinkManagement.Base.Business.Database
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LoginDetails>().HasOne(l => l.UserModel);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetails> Users { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
    }
}