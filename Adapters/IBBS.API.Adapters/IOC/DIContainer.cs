using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Handlers;
using IBBS.API.Adapters.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace IBBS.API.Adapters.IOC;

/// <summary>
/// The Dependency Injection Container class.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Adds the API handlers.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <returns>The service collection interface.</returns>
	public static IServiceCollection AddAPIHandlers(this IServiceCollection services) =>
		services.AddScoped<IAiServicesHandler, AiServicesHandler>()
			.AddScoped<IPostRatingsHandler, PostRatingsHandler>()
			.AddAutoMapper(mapperConfig => mapperConfig.AddProfile<DomainMapperProfile>());
	
}
