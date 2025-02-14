// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
	using InternetBulletin.API.Helpers;
	using InternetBulletin.Shared.Constants;
	using Microsoft.AspNetCore.Mvc;
	using static InternetBulletin.Shared.Constants.ConfigurationConstants;
	using static InternetBulletin.Shared.Helpers.KeyVaultHelper;

	/// <summary>
	/// The Base Controller Class.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	/// <param name="configuration">The Configuration.</param>
	[Authorize]
	public abstract class BaseController(IConfiguration configuration) : ControllerBase
	{
		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IConfiguration _configuration = configuration;

		/// <summary>
		/// Determines whether this instance is authorized.
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance is authorized; otherwise, <c>false</c>.
		/// </returns>
		public async Task<bool> IsAuthorized()
		{
			var requestHeaders = this.HttpContext.Request.Headers;
			var acceptableToken = await GetKeyVaultSecretValueAsync(this._configuration, APIAntiforgeryTokenValue);
			if (string.Equals(requestHeaders[APIAntiforgeryTokenConstant], acceptableToken, StringComparison.Ordinal))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Handles the bad request.
		/// </summary>
		/// <returns>The unauthorized object result</returns>
		public UnauthorizedObjectResult HandleUnAuthorizedRequest()
		{
			return this.Unauthorized(new { status = 401, isSuccess = false, error = ExceptionConstants.UserUnauthorizedMessageConstant });
		}

		/// <summary>
		/// Handles the success result.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns>The ok object result</returns>
		public OkObjectResult HandleSuccessResult(object response)
		{
			return this.Ok(new { status = 200, isSuccess = true, response });
		}

		/// <summary>
		/// Handles the bad request.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>The bad request result.</returns>
		public BadRequestObjectResult HandleBadRequest(string message)
		{
			return this.BadRequest(new { status = 400, isSuccess = false, error = message });
		}
	}
}
