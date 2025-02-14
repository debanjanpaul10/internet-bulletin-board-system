// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Web.Controllers
{
	using static InternetBulletin.Shared.Constants.ConfigurationConstants;
	using InternetBulletin.Web.Helpers;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// The Base Controller Class.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	/// <param name="configuration">The Configuration.</param>
	[Authorize]
	public abstract class BaseController(IConfiguration configuration) : Controller
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
		public bool IsAuthorized()
		{
			var requestHeaders = this.HttpContext.Request.Headers;
			var acceptableToken = KeyVaultHelper.GetKeyValueAsync(this._configuration, WebAntiforgeryTokenValue);
			if (string.Equals(requestHeaders[WebAntiforgeryTokenConstant], acceptableToken, StringComparison.Ordinal))
			{
				return true;
			}

			return false;
		}
	}
}
