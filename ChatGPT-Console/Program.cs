using System;
using System.IO;
using System.Threading.Tasks;
using ChatGPTTools;
using Microsoft.Extensions.Configuration;

namespace ChatGPT_API_Example
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>(); // Use the current class or any class in your project

            Configuration = builder.Build();

            var openAISecrets = Configuration.GetSection("OPEN_AI");
            string openApiOrganization = openAISecrets["ORGANIZATION"];
            string openApiSecretApiKey = openAISecrets["SECRET_API_KEY"];

            // Your code goes here

            var chatGPTClient = new ChatGPTClient(openApiOrganization, openApiSecretApiKey);

            string prompt = "What is the capital of France?";
            string queryResponse = await chatGPTClient.Query(prompt);

            Console.WriteLine("Response: " + queryResponse);
        }
    }
}