# ChatGPT-DotNET

```
// Using the simple client for stateless queries

var chatGPTClient = new ChatGPTClient(openAiOrganization, openAiSecretApiKey);

string prompt = "What is the captial city of Canada?";
string queryResponse = await chatGPTClient.Query(prompt);
```

```
// Using the Conversation Client for stateful queries and conversations

var chatGPTClient = new ChatGPTClient(openAiOrganization, openAiSecretApiKey);
var chatGPTConversationClient = new ChatGPTConversationClient(chatGPTClient);
Console.WriteLine("Now chatting...");

while (true)
{
    Console.WriteLine("Enter Prompt: ");
    prompt = Console.ReadLine();

    queryResponse = await chatGPTConversationClient.Chat(prompt);
    Console.WriteLine(queryResponse);
}
```