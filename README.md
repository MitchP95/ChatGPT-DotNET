# ChatGPT-DotNET

## Setup

### Adding User Secrets

The console application uses user secrets from you developement environment:

https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.usersecrets?view=dotnet-plat-ext-8.0

To set the secrets open a terminal/pwsh in the ChatGPT-Console project directory, and use the following command line commands:

```
dotnet user-secrets set "OPEN_AI:ORGANIZATION" "your-organization"
dotnet user-secrets set "OPEN_AI:SECRET_API_KEY" "your-secret-api-key"
```

These commands use the colon : as a separator to define the nested structure. The resulting secrets.json file will be as follows:

```
{
  "OPEN_AI:ORGANIZATION": "your-organization",
  "OPEN_AI:SECRET_API_KEY": "your-secret-api-key"
}
```
