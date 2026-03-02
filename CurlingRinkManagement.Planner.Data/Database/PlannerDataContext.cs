using CurlingRinkManagement.Planner.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Data.Database;

public class PlannerDataContext : DbContext
{
    public PlannerDataContext()
    {
    }

    public PlannerDataContext(DbContextOptions<PlannerDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Activity>().OwnsMany(a => a.SheetActivities, sheetActivity => {
            sheetActivity.OwnsOne(a => a.ActivityTime);
            sheetActivity.OwnsMany(s => s.LinkedInstructors);
        });
        modelBuilder.Entity<Activity>().HasMany(a => a.CustomerRequests).WithOne(c => c.Activity);

        modelBuilder.Entity<Contact>().HasIndex(c => new { c.ClubId, c.Email }).IsUnique();
        modelBuilder.Entity<Contact>().HasMany(c => c.Tags).WithMany();

    }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<DateTimeRange> DateTimeRanges { get; set; }
    public DbSet<Sheet> Sheets { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CustomerRequest> CustomerRequests { get; set; }

}

