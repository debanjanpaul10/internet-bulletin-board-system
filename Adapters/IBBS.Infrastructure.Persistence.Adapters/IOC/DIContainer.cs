using IBBS.Domain.DrivenPorts;
using IBBS.Infrastructure.Persistence.Adapters.DataServices;
using InternetBulletin.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.Persistence.Adapters.IOC;

/// <summary>
/// The Dependency Injection container.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Adds the data dependencies.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	/// <param name="isDevelopmentMode">if set to <c>true</c> [is development mode].</param>
	public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentMode) =>
		 services.ConfigureSqlDatabase(configuration, isDevelopmentMode).AddDataManagers();

	/// <summary>
	/// Configures the SQL database.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	/// <param name="isDevelopmentMode">if set to <c>true</c> [is development mode].</param>
	private static IServiceCollection ConfigureSqlDatabase(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentMode)
	{
		var sqlConnectionString = isDevelopmentMode
			? configuration[ConfigurationConstants.LocalSqlConnectionStringConstant]
			: configuration[ConfigurationConstants.SqlConnectionStringConstant];
		ArgumentException.ThrowIfNullOrWhiteSpace(sqlConnectionString);

		return services.AddDbContext<SqlDbContext>(options =>
		{
			options.UseSqlServer(
				sqlConnectionString,
				options => options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null));
		});
	}

	/// <summary>
	/// Configures the data manager dependencies.
	/// </summary>
	/// <param name="services">The services.</param>
	private static IServiceCollection AddDataManagers(this IServiceCollection services) =>
		services.AddScoped<IPostsDataService, PostsDataService>()
			.AddScoped<IPostRatingsDataService, PostRatingsDataService>()
			.AddScoped<ICommonDataManager, CommonDataManager>();

}
