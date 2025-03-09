// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Program class from where the execution starts</summary>
// *********************************************************************************

namespace InternetBulletin.API
{
    using Azure.Identity;
    using InternetBulletin.API.Dependencies;
    using Microsoft.OpenApi.Models;
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
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddEnvironmentVariables();

            var miCredentials = builder.Configuration[ManagedIdentityClientIdConstant];
            var credentials = builder.Environment.IsDevelopment()
            ? new DefaultAzureCredential() : new DefaultAzureCredential(new DefaultAzureCredentialOptions
            {
                ManagedIdentityClientId = miCredentials
            });

            builder.ConfigureAzureAppConfiguration(credentials);
            builder.ConfigureServices();
            builder.ConfigureApplicationDependencies();
            builder.ConfigureBusinessManagerDependencies();
            builder.ConfigureDataManagerDependencies();

            var app = builder.Build();
            ConfigureApplication(app);
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Internet Bulletin API",
                    Version = "v1",
                    Description = "API Documentation for Internet Bulletin",
                    Contact = new OpenApiContact
                    {
                        Name = "Debanjan Paul",
                        Email = "debanjanpaul10@gmail.com"
                    }
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
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Internet Bulletin API v1");
                    c.RoutePrefix = "swaggerui";
                });
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


