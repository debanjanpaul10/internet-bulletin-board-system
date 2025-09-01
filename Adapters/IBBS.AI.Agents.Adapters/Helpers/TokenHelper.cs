// *********************************************************************************
//	<copyright file="TokenHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Token helper class.</summary>
// *********************************************************************************

namespace IBBS.AI.Agents.Adapters.Helpers;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

/// <summary>
/// Token helper.
/// </summary>
[ExcludeFromCodeCoverage]
public class TokenHelper
{
	/// <summary>
	/// Gets ibbs ai token async.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="logger">The logger.</param>
	/// <exception cref="Exception">Exception error.</exception>
	public static async Task<string> GetIbbsAiTokenAsync(IConfiguration configuration, ILogger logger)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, string.Empty));
			return string.Empty;
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, string.Empty));
		}
	}
}

