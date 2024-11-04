using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace Testestefanini.Authenticator;

public class SecretManager
{
    public static async Task<string> GetSecret()
    {
        string secretName = "rds!db-8762c73d-1855-4a95-bd6f-f37ae0ce0835";
        string region = "sa-east-1";

        var client = new AmazonSecretsManagerClient("AKIA5WLTTOOH5OQU5DP6", "DhfZC6KPEpXAEaGPrsxeYfJ1f+jIbEk3UUGd3KOC", RegionEndpoint.GetBySystemName(region));

        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT", 
        };

        GetSecretValueResponse response;

        try
        {
            response = await client.GetSecretValueAsync(request);
        }
        catch (Exception e)
        {
            throw e;
        }

        string secret = response.SecretString;

        dynamic secretObject = JsonConvert.DeserializeObject(secret);
        string password = secretObject.password;

        return password;
    }
}