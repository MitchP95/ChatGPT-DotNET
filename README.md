# ChatGPT-DotNET

## NuGet Package

Install the library with the following:

```
dotnet add package ChatGptDotNET --version 1.0.0
```

https://www.nuget.org/packages/ChatGptDotNET/

## Project Setup

### .NET 6

https://dotnet.microsoft.com/en-us/download/dotnet/6.0

### Adding User Secrets

The console application uses user secrets from your developement environment:

https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.usersecretsconfigurationextensions?view=dotnet-plat-ext-6.0

To set the secrets open a terminal/pwsh in the ChatGPT-Console project directory, and use the following command line commands:

Organization Key:
```
dotnet user-secrets set "OPEN_AI:ORGANIZATION" "your-organization"
```

Secret API Key:
```
dotnet user-secrets set "OPEN_AI:SECRET_API_KEY" "your-secret-api-key"
```

The resulting secrets.json file will be as follows:

```
{
  "OPEN_AI:ORGANIZATION": "your-organization",
  "OPEN_AI:SECRET_API_KEY": "your-secret-api-key"
}
```

## Examples

### Simple Client Example: 

This example demonstrates how to use the ChatGPTClient to make stateless queries. It sends a single prompt to ChatGPT and prints the response.

### Conversation Client Example: 

This example showcases how to use the ChatGPTConversationClient for stateful queries and conversations. It utilizes the ChatGPTClient for querying and enables the user to continuously interact with the model. The user types in prompts, and the program returns the corresponding responses from ChatGPT.
