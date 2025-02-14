// *********************************************************************************
//	<copyright file="DIContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Dependencies
{
	using InternetBulletin.API.Helpers;
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Business.Services;
	using InternetBulletin.Data;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.DataServices;
	using InternetBulletin.Shared.Constants;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// The Dependency Injection Container Class.
	/// </summary>
	public static class DIContainer
	{
		/// <summary>
		/// Configures the application dependencies.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="services">The services.</param>
		public static void ConfigureApplicationDependencies(ConfigurationManager configuration, IServiceCollection services)
		{
			var cosmosConnectionString = KeyVaultHelper.GetKeyValueAsync(configuration, ConfigurationConstants.CosmosConnectionStringConstant);
			var containerName = configuration.GetValue<string>(ConfigurationConstants.ContainerNameConstant);
			if (!string.IsNullOrEmpty(cosmosConnectionString) && !string.IsNullOrEmpty(containerName))
			{
				services.AddDbContext<InternetBulletinDbContext>(options =>
				{
					options.UseCosmos(cosmosConnectionString, containerName);
				});
			}
		}

		/// <summary>
		/// Configures the business manager dependencies.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void ConfigureBusinessManagerDependencies(IServiceCollection services)
		{
			services.AddScoped<IPostsService, PostsService>();
		}

		/// <summary>
		/// Configures the data manager dependencies.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void ConfigureDataManagerDependencies(IServiceCollection services)
		{
			services.AddScoped<IPostsDataService, PostsDataService>();
		}
	}
}
