// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Program class from where the execution starts</summary>
// *********************************************************************************

namespace InternetBulletin.API
{
	using InternetBulletin.API.Dependencies;

	/// <summary>
	/// Program class from where the execution starts
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			ConfigureAzureServices.ConfigureAzureAppConfiguration(builder.Configuration);
			ConfigureAzureServices.ConfigureAzureApplicationInsights(builder.Configuration, builder.Services);
			
			ConfigureServices(builder.Services);
			DIContainer.ConfigureApplicationDependencies(builder.Configuration, builder.Services);
			DIContainer.ConfigureBusinessManagerDependencies(builder.Services);
			DIContainer.ConfigureDataManagerDependencies(builder.Services);

			var app = builder.Build();
			ConfigureApplication(app);
		}

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		public static void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication();
			services.AddControllers();
			services.AddOpenApi();
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(p =>
				{
					p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
				});
			});
		}

		/// <summary>
		/// Configures the application.
		/// </summary>
		/// <param name="app">The application.</param>
		public static void ConfigureApplication(WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseCors();
			app.MapControllers();
			app.Run();
		}
	}
}


