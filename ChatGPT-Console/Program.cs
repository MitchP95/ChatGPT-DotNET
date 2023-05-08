using System;
using ChatGPTTools;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace ChatGPT_API_Example
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string secretName = "";
            string region = "";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            try
            {
                response = await client.GetSecretValueAsync(request);
            }
            catch (Exception e)
            {
                // For a list of the exceptions thrown, see
                // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                throw e;
            }

            string secret = response.SecretString;

            // Your code goes here

            var chatGPTClient = new ChatGPTClient(organizationKey, apiKey);

            string prompt = "What is the capital of France?";
            string response = await chatGPTClient.Query(prompt);

            Console.WriteLine("Response: " + response);
        }
    }
}