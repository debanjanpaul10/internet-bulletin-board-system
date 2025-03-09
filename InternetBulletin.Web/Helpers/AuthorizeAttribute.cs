// *********************************************************************************
//	<copyright file="AuthorizeAttribute.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Authorize Attribute Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Web.Helpers
{
	using InternetBulletin.Shared.Constants;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	/// <summary>
	/// The Authorize Attribute Class.
	/// </summary>
	/// <param name="configuration">The Configuration.</param>
	/// <seealso cref="System.Attribute" />
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter" />
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		/// <summary>
		/// Called when [authorization].
		/// </summary>
		/// <param name="context">The authentication context.</param>
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var antiForgeryToken = context.HttpContext.Request.Headers[ConfigurationConstants.WebAntiforgeryTokenConstant];
			if (string.IsNullOrEmpty(antiForgeryToken))
			{
				context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
			}
		}
	}
}
