﻿
namespace IBBS.Domain.Helpers;

/// <summary>
/// The Domain Constants class.
/// </summary>
internal class DomainConstants
{
	/// <summary>
	/// The logging constants class.
	/// </summary>
	internal static class LoggingConstants
	{
		/// <summary>
		/// The method started message constant
		/// </summary>
		public const string MethodStartedMessageConstant = "Method {0} started at {1} for {2}";

		/// <summary>
		/// The method ended message constant
		/// </summary>
		public const string MethodEndedMessageConstant = "Method {0} ended at {1} for {2}";

		/// <summary>
		/// The method failed with message constant.
		/// </summary>
		/// <returns>{0} failed at {1} with {2}</returns>
		public const string MethodFailedWithMessageConstant = "Method {0} failed at {1} with {2}";
	}

	/// <summary>
	/// The exception constants class.
	/// </summary>
	internal static class ExceptionConstants
	{
		/// <summary>
		/// The post not found message constant
		/// </summary>
		internal const string PostNotFoundMessageConstant = "It seems the post you are looking for does not exists anymore!";

		/// <summary>
		/// The post identifier not present message constant
		/// </summary>
		internal const string PostIdNotPresentMessageConstant = "The provided postId is null or empty.";

		/// <summary>
		/// The post unique identifier not exists message constant
		/// </summary>
		internal const string PostGuidNotValidMessageConstant = "The provided postId is not a valid GUID.";
	}
}

/// <summary>
/// The enum for AI Usage details
/// </summary>
public enum AiUsages
{
	/// <summary>
	/// The rewrite story enum.
	/// </summary>
	RewriteStory = 1,

	/// <summary>
	/// The moderate content enum.
	/// </summary>
	ModerateContent = 2,

	/// <summary>
	/// The genre tag enum.
	/// </summary>
	GenreTag = 3,
}
