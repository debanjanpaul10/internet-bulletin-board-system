using IBBS.Domain.DrivingPorts;
using IBBS.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace IBBS.Domain.IOC;

/// <summary>
/// The Dependency Injection Container class.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Adds the domain services.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddDomainServices(this IServiceCollection services) =>
		 services.AddScoped<IAIService, AIService>().AddScoped<IPostRatingsService, PostRatingsService>()
		.AddScoped<IPostsService, PostsService>().AddScoped<ICacheService, CacheService>();

}
