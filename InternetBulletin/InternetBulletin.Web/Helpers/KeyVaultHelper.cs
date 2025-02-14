// *********************************************************************************
//	<copyright file="KeyVaultHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Key Vault Helper Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Web.Helpers
{
	using Azure.Identity;
	using Azure.Security.KeyVault.Secrets;
	using InternetBulletin.Shared.Constants;

	/// <summary>
	/// The Key Vault Helper Class.
	/// </summary>
	public static class KeyVaultHelper
	{
		/// <summary>
		/// Gets the key value asynchronous.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="keyName">Name of the key.</param>
		/// <returns>The key value.</returns>
		public static string GetKeyValueAsync(ConfigurationManager configuration, string keyName)
		{
			var keyVaultUri = configuration.GetValue<string>(ConfigurationConstants.KeyVaultEndpointConstant);
			if (!string.IsNullOrEmpty(keyVaultUri))
			{
				var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

				var secret = client.GetSecretAsync(keyName).GetAwaiter().GetResult();
				return secret.Value.Value;
			}

			return string.Empty;
		}

		/// <summary>
		/// Gets the key value asynchronous.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="keyName">Name of the key.</param>
		/// <returns>The key value.</returns>
		public static string GetKeyValueAsync(IConfiguration configuration, string keyName)
		{
			var keyVaultUri = configuration.GetValue<string>(ConfigurationConstants.KeyVaultEndpointConstant);
			if (!string.IsNullOrEmpty(keyVaultUri))
			{
				var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

				var secret = client.GetSecretAsync(keyName).GetAwaiter().GetResult();
				return secret.Value.Value;
			}

			return string.Empty;
		}
	}
}
