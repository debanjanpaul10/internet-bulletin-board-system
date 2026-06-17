using System.Diagnostics.CodeAnalysis;
using IBBS.Domain.DrivenPorts;
using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using IBBS.Infrastructure.Persistence.Adapters.DataManager;
using IBBS.Infrastructure.Persistence.Adapters.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.Persistence.Adapters.IOC;

/// <summary>
/// The Dependency Injection container.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DIContainer
{
    /// <summary>
    /// Adds the data dependencies.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="isDevelopmentMode">if set to <c>true</c> [is development mode].</param>
    public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentMode)
    {
        var currentSqlServiceProvider = configuration[ConfigurationConstants.CurrentSQLProviderConstant] ?? throw new Exception(ExceptionConstants.DatabaseConnectionNotFound);
        switch (currentSqlServiceProvider)
        {
            case DatabaseConstants.AzureSQLConstant:
                services.ConfigureAzureSqlDatabase(configuration, isDevelopmentMode);
                break;
            case DatabaseConstants.PostgreSQLConstant:
                services.ConfigurePostgreSqlDatabase(configuration);
                break;
        }

        return services.AddDataManagers().AddRepositories();
    }


    /// <summary>
    /// Configures the Postgre SQL database.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns>The service collection.</returns>
    private static IServiceCollection ConfigurePostgreSqlDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnectionString = configuration[ConfigurationConstants.PostgreSQLConnectionStringConstant];
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlConnectionString);

        return services.AddDbContext<SqlDbContext>(options =>
            options.UseNpgsql(
                connectionString: sqlConnectionString,
                npgsqlOptionsAction: options => options.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null)));
    }


    /// <summary>
    /// Configures the Azure SQL database.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="isDevelopmentMode">The is development mode flag.</param>
    /// <returns>The service collection.</returns>
    private static IServiceCollection ConfigureAzureSqlDatabase(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentMode)
    {
        var sqlConnectionString = isDevelopmentMode
            ? configuration[ConfigurationConstants.LocalSqlConnectionStringConstant] : configuration[ConfigurationConstants.AzureSqlConnectionStringConstant];
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlConnectionString);

        return services.AddDbContext<SqlDbContext>(options =>
            options.UseSqlServer(
                connectionString: sqlConnectionString,
                sqlServerOptionsAction: options => options.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null))
        );
    }


    /// <summary>
    /// Configures the data manager dependencies.
    /// </summary>
    /// <param name="services">The services.</param>
    private static IServiceCollection AddDataManagers(this IServiceCollection services) =>
        services.AddScoped<IPostsDataService, PostsDataService>()
            .AddScoped<IPostRatingsDataService, PostRatingsDataService>()
            .AddScoped<ICommonDataManager, CommonDataManager>()
            .AddScoped<IProfilesDataService, ProfilesDataService>();

    /// <summary>
    /// Adds the repositories.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IMasterDataRepository, MasterDataRepository>()
            .AddScoped<IPostRatingsRepository, PostRatingsRepository>()
            .AddScoped<IProfilesRepository, ProfilesRepository>()
            .AddScoped<IPostsRepository, PostsRepository>();

}
