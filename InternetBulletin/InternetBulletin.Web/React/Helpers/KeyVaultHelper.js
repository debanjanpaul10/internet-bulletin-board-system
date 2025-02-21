import { SecretClient } from "@azure/keyvault-secrets";
import { InteractiveBrowserCredential } from "@azure/identity";
import { ConfigurationConstants } from "@helpers/Constants";

const keyvaultName = ConfigurationConstants.KeyVaultName;
const vaultUrl = `https://${keyvaultName}.vault.azure.net`;
const secretClient = new SecretClient(
	vaultUrl,
	new InteractiveBrowserCredential({
		clientId: ConfigurationConstants.ClientId,
	})
);

/**
 * Gets the Key vault secret from azure key vault.
 * @param {string} secretName The secret name.
 * @returns {string} The secret value.
 */
export const GetKeyVaultSecret = async (secretName) => {
	try {
		const secret = await secretClient.getSecret(secretName);
		return secret.value;
	} catch (error) {
		console.error(error);
	}
};
