using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore;

public class CMSDbContext(DbContextOptions<CMSDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<ApiComponent> ApiComponents { get; set; }
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<ApiKeyUsage> ApiKeyUsages { get; set; }
    public DbSet<BasicComponent> BasicComponents { get; set; }
    public DbSet<Component> Components { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CMSDbContext).Assembly);

        SeedData(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            // Fallback configuration for development/testing
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=CCMS;Trusted_Connection=true;MultipleActiveResultSets=true");

        // Enable sensitive data logging in development
        #if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        #endif
    }


    private void SeedData(ModelBuilder modelBuilder)
    {
        // TODO: Add seed data here
        // Example:
        // modelBuilder.Entity<YourEntity>().HasData(
        //     new YourEntity { Id = 1, Name = "Sample Entity 1" },
        //     new YourEntity { Id = 2, Name = "Sample Entity 2" }
        // );
    }
}