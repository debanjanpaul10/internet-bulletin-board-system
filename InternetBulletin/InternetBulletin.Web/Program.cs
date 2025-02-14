// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Program class from where the execution starts</summary>
// *********************************************************************************

namespace InternetBulletin.Web
{
	using InternetBulletin.Web.Configuration;
	using InternetBulletin.Web.Helpers;
	using static InternetBulletin.Shared.Constants.ConfigurationConstants;

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

			ConfigureAzureServices.ConfigureAzureAppConfig(builder.Configuration);
			ConfigureAzureServices.ConfigureAzureKeyVault(builder.Configuration);
			ConfigureAzureServices.ConfigureAzureApplicationInsights(builder.Configuration, builder.Services);
			ConfigureServices(builder.Services, builder.Configuration);

			var app = builder.Build();
			ConfigureApplication(app);
		}

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
		{
			services.AddAuthentication();
			services.AddControllers();
			services.AddHttpClient<IHttpClientHelper, HttpClientHelper>(BulletinHttpClientConstant, client =>
			{
				client.BaseAddress = new Uri(configuration.GetValue<string>(WebApiBaseAddressConstant)!);
				client.DefaultRequestHeaders.Add(WebAntiforgeryTokenConstant, KeyVaultHelper.GetKeyValueAsync(configuration, WebAntiforgeryTokenValue));
			});
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(p =>
				{
					p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
				});
			});

			services.AddScoped<IHttpClientHelper, HttpClientHelper>();
		}

		/// <summary>
		/// Configures the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		private static void ConfigureApplication(WebApplication app)
		{
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors();

			app.MapControllerRoute(
			name: "default",
			pattern: "{controller=InternetBulletinWeb}/{action=Index}/{id?}");

			app.Run();
		}
	}
}


