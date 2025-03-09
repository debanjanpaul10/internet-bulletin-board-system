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
    using Polly;
    using Polly.Extensions.Http;
    using Azure.Identity;

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
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.development.json", optional: true).AddEnvironmentVariables();

            var miCredentials = builder.Configuration[ManagedIdentityClientIdConstant];
            var credentials = builder.Environment.IsDevelopment()
                ? new DefaultAzureCredential()
                : new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ManagedIdentityClientId = miCredentials
                });

            builder.ConfigureAzureAppConfig(credentials);
            builder.ConfigureServices();

            var app = builder.Build();
            ConfigureApplication(app);
        }

        /// <summary>
        /// Configures services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();

            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            var webApiUrl = builder.Configuration[WebApiBaseAddressConstant];
            var webApiAntiforgeryToken = builder.Configuration[APIAntiforgeryTokenValue];
            if (!string.IsNullOrEmpty(webApiUrl) && !string.IsNullOrEmpty(webApiAntiforgeryToken))
            {
                builder.Services.AddHttpClient(BulletinHttpClientConstant, client =>
                {
                    client.BaseAddress = new Uri(webApiUrl);
                    client.DefaultRequestHeaders.Add(APIAntiforgeryTokenConstant, webApiAntiforgeryToken);
                    client.Timeout = TimeSpan.FromMinutes(3);
                }).AddPolicyHandler(retryPolicy);
            }

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
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


