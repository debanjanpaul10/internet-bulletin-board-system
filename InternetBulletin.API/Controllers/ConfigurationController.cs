// *********************************************************************************
//	<copyright file="ConfigurationController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Configurations Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
	using InternetBulletin.Shared.Constants;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// The Configurations Controller Class.
	/// </summary>
	/// <seealso cref="InternetBulletin.API.Controllers.BaseController" />
	/// <param name="configuration">The Configuration.</param>
	/// <param name="logger">The Logger</param>
	[ApiController]
	[Route(RouteConstants.ConfigurationBase_RoutePrefix)]
	public class ConfigurationController(IConfiguration configuration, ILogger<ConfigurationController> logger) : BaseController(configuration)
	{
		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<ConfigurationController> _logger = logger;

		/// <summary>
		/// The configuration.
		/// </summary>
		private readonly IConfiguration _configuration = configuration;

		/// <summary>
		/// Gets the key value from configurations.
		/// </summary>
		/// <param name="keyName">The key name.</param>
		/// <returns>The action result of the JSON response.</returns>
		[HttpGet]
		[Route(RouteConstants.GetConfiguration_Route)]
		public IActionResult GetConfigurationValue(string keyName)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetConfigurationValue), DateTime.UtcNow, keyName));
				if (string.IsNullOrEmpty(keyName))
				{
					this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetConfigurationValue), DateTime.UtcNow, "Key name is null or empty"));
					return this.BadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
				}

				var keyValue = this._configuration.GetValue<string>(keyName);
				if (string.IsNullOrEmpty(keyValue))
				{
					return this.BadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
				}

				return this.HandleSuccessResult(keyValue);
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetConfigurationValue), DateTime.UtcNow, ex.Message));
				return this.HandleBadRequest(ex.Message);
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetConfigurationValue), DateTime.UtcNow, keyName));
			}
		}
	}

}