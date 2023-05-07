using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChatGPTTools
{
    public class ChatGPTClient
    {
        private readonly string _organizationKey;
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public ChatGPTClient(string organizationKey, string apiKey)
        {
            _httpClient = new HttpClient();
            _organizationKey = organizationKey;
            _apiKey = apiKey;
        }

        public async Task<string> Query(string content, int maxTokens = 50)
        {
            var requestContent = new JObject
            {
                {"model", "gpt-3.5-turbo"},
                {
                    "messages", new JArray
                    {
                        new JObject
                        {
                            {"role", "user"},
                            {"content", content}
                        }
                    }
                },
                {"max_tokens", maxTokens}
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(requestContent.ToString(), System.Text.Encoding.UTF8, "application/json")
            };

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            requestMessage.Headers.Add("OpenAI-Organization", _organizationKey);

            try
            {
                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JObject.Parse(responseContent);

                return responseObject["choices"]?.First?["message"]?["content"]?.ToString().Trim() ?? string.Empty;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
            {
                // You can return an appropriate message or implement a retry mechanism with a delay
                return "Rate limit exceeded. Please wait before sending more requests.";
            }
        }
    }
}