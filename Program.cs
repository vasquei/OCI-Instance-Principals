using System;
using System.Threading.Tasks;
using Oci.Common;
using Oci.Common.Auth;
using Oci.VaultService;
using Oci.VaultService.Requests;
using Oci.KeymanagementService;
using Oci.KeymanagementService.Requests;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Instance Principal Authentication Provider
            var provider = new InstancePrincipalsAuthenticationDetailsProvider();

            // Compartment OCID (replace with yours)
            string compartmentId = "ocid1.compartment.oc1..xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

            // --- List Vaults (KMS Vault Client) ---
            var kmsVaultClient = new KmsVaultClient(provider, new ClientConfiguration());

            var listVaultsRequest = new ListVaultsRequest
            {
                CompartmentId = compartmentId
            };

            var vaultsResponse = await kmsVaultClient.ListVaults(listVaultsRequest);

            Console.WriteLine("=== VAULTS ===");
            foreach (var vault in vaultsResponse.Items)
            {
                Console.WriteLine($"Vault ID: {vault.Id}");
                Console.WriteLine($"Vault Name: {vault.DisplayName}");
                Console.WriteLine();
            }

            // --- List Secrets (Vaults Client) ---
            var vaultsClient = new VaultsClient(provider, new ClientConfiguration());

            var listSecretsRequest = new ListSecretsRequest
            {
                CompartmentId = compartmentId
            };

            var secretsResponse = await vaultsClient.ListSecrets(listSecretsRequest);

            Console.WriteLine("=== SECRETS ===");
            foreach (var secret in secretsResponse.Items)
            {
                Console.WriteLine($"Secret ID: {secret.Id}");
                Console.WriteLine($"Secret Name: {secret.SecretName}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
