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
    using InternetBulletin.Shared.Helpers;
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
        /// <summary>
        /// Configures the application's DbContext to use SQL Server with environment-specific connection strings and retry-on-failure settings.
        /// </summary>
        public static void ConfigureAzureSqlServer(this WebApplicationBuilder builder)
        {
            var sqlConnectionString = builder.Environment.IsDevelopment()
                ? builder.Configuration[ConfigurationConstants.LocalSqlConnectionStringConstant]
                : builder.Configuration[ConfigurationConstants.SqlConnectionStringConstant];
            if (!string.IsNullOrEmpty(sqlConnectionString))
            {
                builder.Services.AddDbContext<SqlDbContext>(options =>
                {
                    options.UseSqlServer
                    (
                        connectionString: sqlConnectionString,
                        options => options.EnableRetryOnFailure
                        (
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        )
                    );
                });
            }
        }

        /// <summary>
        /// Configures business manager dependencies.
        /// </summary>
        /// <summary>
        /// Registers business layer services with scoped lifetimes in the dependency injection container.
        /// </summary>
        /// <param name="builder">The web application builder used to configure services.</param>
        public static void ConfigureBusinessManagerDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
            builder.Services.AddScoped<IPostsService, PostsService>();
            builder.Services.AddScoped<IProfilesService, ProfilesService>();
            builder.Services.AddScoped<IPostRatingsService, PostRatingsService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ICacheService, CacheService>();
        }

        /// <summary>
        /// Configures data manager dependencies.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        public static void ConfigureDataManagerDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostsDataService, PostsDataService>();
            builder.Services.AddScoped<IProfilesDataService, ProfilesDataService>();
            builder.Services.AddScoped<IPostRatingsDataService, PostRatingsDataService>();
            builder.Services.AddScoped<IUsersDataService, UsersDataService>();
        }
    }
}
