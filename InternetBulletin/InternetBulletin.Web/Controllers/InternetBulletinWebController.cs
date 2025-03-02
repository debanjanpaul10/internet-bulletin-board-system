// *********************************************************************************
//	<copyright file="InternetBulletinWebController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Internet Bulletin Web Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Web.Controllers
{
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Web.Helpers;
	using Microsoft.AspNetCore.Mvc;
	using System.Globalization;
	using System.Text;

	/// <summary>
	/// The Internet Bulletin Web Controller Class.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	/// <param name="httpClientHelper">The HTTP client helper</param>
	/// <param name="configuration">The Configuration parameter.</param>
	[Route("[controller]")]
	public class InternetBulletinWebController(
		IHttpClientHelper httpClientHelper,
		IConfiguration configuration,
		ILogger<InternetBulletinWebController> logger) : BaseController(configuration)
	{
		/// <summary>
		/// The HTTP client helper
		/// </summary>
		private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<InternetBulletinWebController> _logger = logger;

		/// <summary>
		/// Gets the resource data asynchronous.
		/// </summary>
		/// <param name="resourceUrl">The resource URL.</param>
		/// <returns>The action result of the JSON response.</returns>
		[HttpGet]
		[Route(RouteConstants.GetResourceUrl_RoutePrefix)]
		public async Task<IActionResult> GetResourceDataAsync(string resourceUrl)
		{
			try
			{
				this._logger.LogInformation(string.Format(
					CultureInfo.InvariantCulture, LoggingConstants.LogHelperMethodStart, nameof(GetResourceDataAsync), DateTime.UtcNow, resourceUrl));

				if (await this.IsAuthorized())
				{
					var response = await this._httpClientHelper.GetAsync(resourceUrl);
					if (response.IsSuccessStatusCode)
					{
						var responseBody = await response.Content.ReadAsStringAsync();
						return this.Ok(responseBody);
					}
					else
					{
						var responseBody = await response.Content.ReadAsStringAsync();
						return this.StatusCode((int)response.StatusCode, responseBody);
					}
				}

				return this.Unauthorized();
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(
					CultureInfo.InvariantCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetResourceDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(
					CultureInfo.InvariantCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetResourceDataAsync), DateTime.UtcNow, resourceUrl));
			}
		}

		/// <summary>
		/// Posts the resource data asynchronous.
		/// </summary>
		/// <param name="resourceUrl">The resource URL.</param>
		/// <returns>The action result of JSON response.</returns>
		[HttpPost]
		[Route(RouteConstants.PostResourceUrl_RoutePrefix)]
		public async Task<IActionResult> PostResourceDataAsync(string resourceUrl)
		{
			try
			{
				this._logger.LogInformation(string.Format(
					CultureInfo.InvariantCulture, LoggingConstants.LogHelperMethodStart, nameof(PostResourceDataAsync), DateTime.UtcNow, resourceUrl));

				if (await this.IsAuthorized())
				{
					var bodyString = string.Empty;
					var request = this.HttpContext.Request;
					using (var reader = new StreamReader(request.Body, Encoding.UTF8))
					{
						bodyString = await reader.ReadToEndAsync();
					}

					var response = await this._httpClientHelper.PostAsync(resourceUrl, bodyString);
					if (response.IsSuccessStatusCode)

					{
						var responseBody = await response.Content.ReadAsStringAsync();
						return this.Ok(responseBody);
					}
					else
					{
						var responseBody = await response.Content.ReadAsStringAsync();
						return this.StatusCode((int)response.StatusCode, responseBody);
					}
				}
				return this.Unauthorized();
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(
					CultureInfo.InvariantCulture, LoggingConstants.LogHelperMethodFailed, nameof(PostResourceDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(
					CultureInfo.InvariantCulture, LoggingConstants.LogHelperMethodEnded, nameof(PostResourceDataAsync), DateTime.UtcNow, resourceUrl));
			}
		}
	}
}
