using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChatGPTTools
{
    public class ChatGPTClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public ChatGPTClient(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;

            TestAuthorizationAsync().Wait();
        }

        private async Task TestAuthorizationAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.openai.com/v1/engines/davinci-codex")
            {
                Headers = { Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey) }
            };

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Invalid API key or not authorized.");
            }
        }

        public async Task<string> Query(string prompt)
        {
            var requestContent = new JObject
            {
                {"prompt", prompt}
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/engines/davinci-codex/completions")
            {
                Content = new StringContent(requestContent.ToString(), System.Text.Encoding.UTF8, "application/json")
            };

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseContent);

            return responseObject["choices"]?.First?["text"]?.ToString().Trim() ?? string.Empty;
        }
    }
}