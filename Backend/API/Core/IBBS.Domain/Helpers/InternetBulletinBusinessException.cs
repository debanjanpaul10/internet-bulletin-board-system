using System.Diagnostics.CodeAnalysis;

namespace IBBS.Domain.Helpers;

/// <summary>
/// Internet bulletin exception helper.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="InternetBulletinBusinessException"/> class.
/// </remarks>
/// <param name="statusCode">The status code.</param>
/// <param name="exceptionMessage">The exception message.</param>
/// <param name="details">The details.</param>
[ExcludeFromCodeCoverage]
public class InternetBulletinBusinessException(int statusCode, string exceptionMessage, string details)
{
	/// <summary>
	/// Gets or sets the status code.
	/// </summary>
	/// <value>
	/// The status code.
	/// </value>
	public int StatusCode { get; set; } = statusCode;

	/// <summary>
	/// Gets or sets the message.
	/// </summary>
	/// <value>
	/// The message.
	/// </value>
	public string? ExceptionMessage { get; set; } = exceptionMessage;

	/// <summary>
	/// Gets or sets the details.
	/// </summary>
	/// <value>
	/// The details.
	/// </value>
	public string? Details { get; set; } = details;
}


