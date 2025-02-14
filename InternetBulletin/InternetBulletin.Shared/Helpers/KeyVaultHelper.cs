// *********************************************************************************
//	<copyright file="KeyVaultHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Key Vault Helper Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Helpers
{
	using Azure.Identity;
	using Azure.Security.KeyVault.Secrets;
	using InternetBulletin.Shared.Constants;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	/// The Key Vault Helper Class.
	/// </summary>
	public static class KeyVaultHelper
	{
		/// <summary>
		/// Gets the key vault secret value asynchronous.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="keyName">Name of the key.</param>
		/// <returns>The secret value.</returns>
		public static string GetKeyVaultSecretValueAsync(IConfiguration configuration, string keyName)
		{
			var keyVaultUrl = configuration.GetValue<string>(ConfigurationConstants.KeyVaultEndpointConstant);
			if (!string.IsNullOrEmpty(keyVaultUrl))
			{
				var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
				var secret = secretClient.GetSecretAsync(keyName).GetAwaiter().GetResult();
				return secret.Value.Value;
			}

			return string.Empty;
		}
	}
}
