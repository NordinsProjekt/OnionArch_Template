using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EFCore;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the CMSDbContext to the service collection with SQL Server
    /// </summary>
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CMSDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    /// <summary>
    ///     Adds the CMSDbContext to the service collection with a custom connection string
    /// </summary>
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CMSDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }

    /// <summary>
    ///     Adds the CMSDbContext to the service collection with custom configuration
    /// </summary>
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
        Action<DbContextOptionsBuilder> configureOptions)
    {
        services.AddDbContext<CMSDbContext>(configureOptions);
        return services;
    }

    /// <summary>
    ///     Adds the CMSDbContext to the service collection with SQL Server and custom options
    /// </summary>
    public static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services,
        string connectionString,
        Action<SqlServerDbContextOptionsBuilder>? sqlServerOptions = null)
    {
        services.AddDbContext<CMSDbContext>(options => { options.UseSqlServer(connectionString, sqlServerOptions); });

        return services;
    }

    /// <summary>
    ///     Adds the CMSDbContext with in-memory database (for testing)
    /// </summary>
    public static IServiceCollection AddInMemoryApplicationDbContext(this IServiceCollection services,
        string databaseName = "TestDb")
    {
        services.AddDbContext<CMSDbContext>(options =>
            options.UseInMemoryDatabase(databaseName));

        return services;
    }
}