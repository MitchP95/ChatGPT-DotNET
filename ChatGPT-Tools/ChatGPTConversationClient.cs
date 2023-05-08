using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
namespace ChatGPTTools
{
    public class ChatGPTConversationClient
    {
        private const string USER = "User";
        private const string SYSTEM = "System";

        private readonly ChatGPTClient _simpleClient;
        private List<string> _conversation = new List<string>();

        public ChatGPTConversationClient(ChatGPTClient simpleClient)
        {
            _simpleClient = simpleClient;
        }

        /// <summary>
        /// Starts or continues a conversation
        /// </summary>
        /// <param name="content"></param>
        /// <param name="maxTokens"></param>
        /// <returns></returns>
        public async Task<string> Chat(string content, int maxTokens = 50)
        {
            _conversation.Add($"{USER}: {content}");
            string response = await _simpleClient.Query(string.Join(Environment.NewLine, _conversation), maxTokens);
            _conversation.Add($"{SYSTEM}: {response}");

            return response;
        }
    }
}
