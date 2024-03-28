using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Security.KeyVault.Certificates;

namespace ConnectedVehicleAPI.Service.Secrets
{
    public class AzKeyVaultSecrets
    {
        /// <summary>
        /// Get secrets using key by DefaultAzureCredential
        /// </summary>
        /// <returns>return secret value</returns>
        public static async Task<string> GetUsingSecretClient(string secretKey)
        {
            var credential = new DefaultAzureCredential();
            var secretClient = new SecretClient(new Uri("secret idetity url"), credential);
            var secret = await secretClient.GetSecretAsync(secretKey);
            return secret.Value.Value;
        }

        /// <summary>
        /// Get key vault value
        /// </summary>
        /// <returns>return keyvault value</returns>
        public static async Task<KeyVaultSecret> GetUsingKeyVaultClient()
        {
            var azureCredentialOption = new DefaultAzureCredentialOptions();
            var credential = new DefaultAzureCredential(azureCredentialOption);
            var keyVaultClient = new SecretClient(new Uri("https://myvault.vault.azure.net/"), credential);

            var secret = await keyVaultClient.GetSecretAsync("");
            return secret.Value;
        }
    }
}
