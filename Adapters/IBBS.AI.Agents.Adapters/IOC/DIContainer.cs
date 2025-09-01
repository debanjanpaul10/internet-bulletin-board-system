using IBBS.AI.Agents.Adapters.AgentManagers;
using IBBS.AI.Agents.Adapters.Helpers;
using IBBS.Domain.DrivenPorts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.IOC;

/// <summary>
/// The Dependency Injection container class.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Adds the ai agents services.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddAiAgentsServices(this IServiceCollection services, IConfiguration configuration)
	{
		return services.AddScoped<IHttpClientHelper, HttpClientHelper>()
			.AddScoped<IAiAgentsService, AiAgentsService>().ConfigureHttpClientFactory(configuration);
	}

	// <summary>
	/// Configures the HTTP client factory.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	private static IServiceCollection ConfigureHttpClientFactory(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient(ConfigurationConstants.AiAgentsHttpClient, client =>
		{
			var apiBaseAddress = configuration[ConfigurationConstants.AiAgentsApiBaseUrl];
			if (string.IsNullOrEmpty(apiBaseAddress))
			{
				throw new ArgumentNullException(apiBaseAddress);
			}

			client.BaseAddress = new Uri(apiBaseAddress);
			client.Timeout = TimeSpan.FromMinutes(3);
		});

		return services;
	}
}
