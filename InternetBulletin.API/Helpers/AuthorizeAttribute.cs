// *********************************************************************************
//	<copyright file="AuthorizeAttribute.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Authorize Attribute Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Helpers
{
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
            var authenticationToken = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(authenticationToken))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

        }
    }
}
