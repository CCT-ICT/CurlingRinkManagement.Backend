using CurlingRinkManagement.Core.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Core.Data.Database;

public class CoreDataContext : DbContext
{
    public CoreDataContext()
    {
    }

    public CoreDataContext(DbContextOptions<CoreDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Club> Clubs { get; set; }
}

