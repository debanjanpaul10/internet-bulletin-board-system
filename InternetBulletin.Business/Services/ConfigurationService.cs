// *********************************************************************************
//	<copyright file="ConfigurationService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Configuration service class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The Configuration Service Class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="logger">The logger.</param>
    /// <seealso cref="IConfigurationService"/>
    public class ConfigurationService(IConfiguration configuration, ILogger<ConfigurationService> logger) : IConfigurationService
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ConfigurationService> _logger = logger;

        /// <summary>
        /// Gets configuration value.
        /// </summary>
        /// <param name="keyName">The key name.</param>
        public string GetConfigurationValue(string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                var exception = new Exception(ExceptionConstants.ConfigurationValueIsEmptyMessageConstant);
                this._logger.LogError(exception, exception.Message);
                throw exception;
            }

            var keyValue = this._configuration[keyName];
            if (string.IsNullOrEmpty(keyValue))
            {
                var exception = new Exception(ExceptionConstants.ConfigurationValueNotExistsMessageConstant);
                this._logger.LogError(exception, exception.Message);
                throw exception;
            }
            else
            {
                return keyValue;
            }
        }
    }
}