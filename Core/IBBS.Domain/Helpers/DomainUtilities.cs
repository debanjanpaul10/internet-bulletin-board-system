using IBBS.Domain.DomainEntities.Posts;
using Microsoft.Extensions.Logging;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.Helpers;

/// <summary>
/// The Domain utilities.
/// </summary>
internal static class DomainUtilities
{
	/// <summary>
	/// Throws if null.
	/// </summary>
	/// <param name="obj">The obj.</param>
	/// <param name="message">The message.</param>
	/// <param name="commonLogger">The common logger.</param>
	/// <typeparam name="T">The null variable type</typeparam>
	/// <typeparam name="L">The logger type.</typeparam>
	internal static T ThrowIfNull<T, L>(T obj, string message, ILogger<L> commonLogger)
	{
		if (obj is null)
		{
			ThrowLoggedException(message, commonLogger);
		}
		return obj;
	}

	/// <summary>
	/// Throws logged exception.
	/// </summary>
	/// <param name="message">The message.</param>
	/// <param name="commonLogger">The common logger.</param>
	/// <typeparam name="T"></typeparam>
	internal static void ThrowLoggedException<T>(string message, ILogger<T> commonLogger)
	{
		var exception = new Exception(message);
		commonLogger.LogError(exception, exception.Message);
		throw exception;
	}

	/// <summary>
	/// Validates and parse post id.
	/// </summary>
	/// <param name="postId">The post id.</param>
	/// <param name="logger">The logger.</param>
	/// <typeparam name="T">The logger type.</typeparam>
	internal static Guid ValidateAndParsePostId<T>(string postId, ILogger<T> logger)
	{
		if (string.IsNullOrWhiteSpace(postId))
		{
			ThrowLoggedException(ExceptionConstants.PostIdNotPresentMessageConstant, logger);
		}

		if (!Guid.TryParse(postId, out var postGuid))
		{
			ThrowLoggedException(ExceptionConstants.PostGuidNotValidMessageConstant, logger);
		}

		return postGuid;
	}

	/// <summary>
	/// Creates update post dto.
	/// </summary>
	/// <param name="post">The post.</param>
	internal static UpdatePostDomain CreateUpdatePostDTO(PostDomain post)
	{
		return new UpdatePostDomain
		{
			PostContent = post.PostContent,
			PostRating = post.Ratings,
			PostId = post.PostId,
			PostTitle = post.PostTitle
		};
	}
}
