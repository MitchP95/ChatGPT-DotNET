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
            string openAiOrganization = openAISecrets["ORGANIZATION"];
            string openAiSecretApiKey = openAISecrets["SECRET_API_KEY"];

            if (string.IsNullOrEmpty(openAiOrganization) || string.IsNullOrEmpty(openAiSecretApiKey))
            {
                Console.WriteLine("The OpenAI Organization / Secret API Key are not set - Follow the README for setup instructions.");
                Environment.Exit(1);
            }

            // Using the simple client for stateless queries

            var chatGPTClient = new ChatGPTClient(openAiOrganization, openAiSecretApiKey);

            string prompt = "What is the captial city of Canada?";
            string queryResponse = await chatGPTClient.Query(prompt);

            Console.WriteLine("Response: " + queryResponse);

            Console.WriteLine(Environment.NewLine);

            // Using the Conversation Client for stateful queries and conversations

            var chatGPTConversationClient = new ChatGPTConversationClient(chatGPTClient);
            Console.WriteLine("Now chatting...");

            while (true)
            {
                Console.WriteLine("Enter Prompt: ");
                prompt = Console.ReadLine();

                queryResponse = await chatGPTConversationClient.Chat(prompt);
                Console.WriteLine(queryResponse);
            }
        }
    }
}
