using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    // TODO: Add your DbSet properties here
    // Example:
    // public DbSet<YourEntity> YourEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        SeedData(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Fallback configuration for development/testing
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OnionArchDb;Trusted_Connection=true;MultipleActiveResultSets=true");
        }

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