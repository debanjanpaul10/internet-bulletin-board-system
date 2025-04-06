// *********************************************************************************
//	<copyright file="DIContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Dependencies
{
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
        public static void ConfigureApplicationDependencies(this WebApplicationBuilder builder)
        {
            var cosmosConnectionString = builder.Configuration[ConfigurationConstants.CosmosConnectionStringConstant];
            var databaseName = builder.Configuration[ConfigurationConstants.CosmosDatabaseNameConstant];
            if (!string.IsNullOrEmpty(cosmosConnectionString) && !string.IsNullOrEmpty(databaseName))
            {
                builder.Services.AddDbContext<CosmosDbContext>(options =>
                {
                    options.UseCosmos(cosmosConnectionString, databaseName);
                });
            }

            var sqlConnectionString = builder.Configuration[ConfigurationConstants.SqlConnectionStringConstant];
            if (!string.IsNullOrEmpty(sqlConnectionString))
            {
                builder.Services.AddDbContext<SqlDbContext>(options =>
                {
                    options.UseSqlServer(connectionString: sqlConnectionString);
                });
            }
        }

        /// <summary>
        /// Configures business manager dependencies.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureBusinessManagerDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostsService, PostsService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
            builder.Services.AddScoped<IProfilesService, ProfilesService>();
        }

        /// <summary>
        /// Configures data manager dependencies.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void ConfigureDataManagerDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostsDataService, PostsDataService>();
            builder.Services.AddScoped<IUsersDataService, UsersDataService>();
            builder.Services.AddScoped<IProfilesDataService, ProfilesDataService>();
        }
    }
}
