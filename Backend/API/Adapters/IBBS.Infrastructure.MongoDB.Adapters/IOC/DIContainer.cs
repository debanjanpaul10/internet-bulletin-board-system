using IBBS.Domain.DrivenPorts;
using IBBS.Infrastructure.MongoDB.Adapters.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using static IBBS.Infrastructure.MongoDB.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.MongoDB.Adapters.IOC;

/// <summary>
/// The Dependency Injection Container class.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Adds the mongo database adapter dependencies.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddMongoDbAdapterDependencies(this IServiceCollection services, IConfiguration configuration) =>
		services.ConfigureMongoDbServer(configuration)
		.AddDataManagers().AddRepositories();

	/// <summary>
	/// Configures the mongo database server.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	/// <returns>The service collection.</returns>
	private static IServiceCollection ConfigureMongoDbServer(this IServiceCollection services, IConfiguration configuration)
	{
		var mongoDbConnectionString = configuration[ConfigurationConstants.AiAgentsLabMongoConnectionString];
		ArgumentException.ThrowIfNullOrWhiteSpace(mongoDbConnectionString, nameof(mongoDbConnectionString));

		try
		{
			var settings = MongoClientSettings.FromConnectionString(mongoDbConnectionString);
			settings.UseTls = true;
			settings.SslSettings = new SslSettings()
			{
				EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
				CheckCertificateRevocation = false
			};

			services.AddSingleton<IMongoClient>(new MongoClient(settings));
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to configure MongoDB client. Connection string format may be incorrect. Error: {ex.Message}", ex);
		}

		return services;
	}

	/// <summary>
	/// Adds the data managers.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>The updated service collection.</returns>
	private static IServiceCollection AddDataManagers(this IServiceCollection services) =>
		services;

	/// <summary>
	/// Adds the data repositories.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>The updated service collection.</returns>
	private static IServiceCollection AddRepositories(this IServiceCollection services) =>
		services.AddScoped<IMongoDatabaseRepository, IMongoDatabaseRepository>();
}
