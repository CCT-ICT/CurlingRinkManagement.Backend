using CurlingRinkManagement.Planner.Domain.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Business.Database;

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
        modelBuilder.Entity<Activity>().OwnsMany(a => a.PlannedDates);
        modelBuilder.Entity<Activity>().OwnsMany(a => a.Sheets);
    }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<DateTimeRange> DateTimeRanges { get; set; }
    public DbSet<Sheet> Sheets { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
}

