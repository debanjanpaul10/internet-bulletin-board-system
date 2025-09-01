namespace IBBS.API.Helpers;

/// <summary>
/// The Swagger constants class.
/// </summary>
internal static class SwaggerConstants
{
	/// <summary>
	/// The Swagger UI Constants
	/// </summary>
	internal static class SwaggerUIConstants
	{
		// <summary>
		/// The api version for Swagger documentation.
		/// </summary>
		internal const string ApiVersion = "v1";

		/// <summary>
		/// The swagger endpoint for the API documentation.
		/// </summary>
		internal const string SwaggerEndpointUrl = "/swagger/v1/swagger.json";

		/// <summary>
		/// The swagger ui endpoint prefix.
		/// </summary>
		internal const string SwaggerUiPrefix = "swaggerui";

		/// <summary>
		/// The description for the Swagger documentation.
		/// </summary>
		internal const string SwaggerDescription = "API documentation for Internet Bulletin Board Service";

		/// <summary>
		/// The Author Details class contains information about the author of the API.
		/// </summary>
		internal static class AuthorDetails
		{
			/// <summary>
			/// The author's name.
			/// </summary>
			internal static readonly string Name = "Debanjan Paul";

			/// <summary>
			/// The author's email address.
			/// </summary>
			internal static readonly string Email = "debanjanpaul10@gmail.com";
		}

		/// <summary>
		/// The API name for Swagger documentation.
		/// </summary>
		internal const string ApplicationAPIName = "IBBS.API";
	}
}
