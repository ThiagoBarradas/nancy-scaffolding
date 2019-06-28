[![Codacy Badge](https://api.codacy.com/project/badge/Grade/37ba381310434bd0af7657bf62e2f5de)](https://www.codacy.com/app/ThiagoBarradas/nancy-scaffolding?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=ThiagoBarradas/nancy-scaffolding&amp;utm_campaign=Badge_Grade)
[![Build status](https://ci.appveyor.com/api/projects/status/q997fiwtuv17tvtx/branch/master?svg=true)](https://ci.appveyor.com/project/ThiagoBarradas/nancy-scaffolding/branch/master)
[![codecov](https://codecov.io/gh/ThiagoBarradas/nancy-scaffolding/branch/master/graph/badge.svg)](https://codecov.io/gh/ThiagoBarradas/nancy-scaffolding)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Nancy.Scaffolding.svg)](https://www.nuget.org/packages/Nancy.Scaffolding/)
[![NuGet Version](https://img.shields.io/nuget/v/Nancy.Scaffolding.svg)](https://www.nuget.org/packages/Nancy.Scaffolding/)

# Nancy.Scaffolding

Build web api fast and easily.

# Startup Application

Configure service in statup
```c#
// Program.cs

class Program
{
    static void Main(string[] args)
    {
        var config = new ApiBasicConfiguration
        {
            ApplicationContainer = RegisterApplicationContainer,
            RequestContainer = RegisterRequestContainer,
            Pipelines = ConfigurePipelines,
            Mapper = ConfigureMapper,
            ApiName = "My App",
            ApiPort = 5855,
            EnvironmentVariablesPrefix = "Prefix_"
        };

        Api.Run(config);
    }

    public static TinyIoCContainer RegisterApplicationContainer(TinyIoCContainer container)
    {
        return container;
    }

    public static TinyIoCContainer RegisterRequestContainer(NancyContext context, TinyIoCContainer container)
    {
        return container;
    }

    public static IPipelines ConfigurePipelines(IPipelines pipelines, TinyIoCContainer container)
    {
        return pipelines;
    }

    public static IMapperConfigurationExpression ConfigureMapper(IMapperConfigurationExpression config, TinyIoCContainer container)
    {
        return config;
    }
}

```

App Settings
```json
// appsettings.{environment}.json

{
  "ApiSettings": {
    "AppUrl": "http://localhost:5855",
    "JsonSerializer": "Snakecase",
    "PathPrefix": "/myapp/v1",
    "Domain": "MyDomain",
    "Application": "MyApp",
    "Version": "v1",
    "SupportedCultures": [ "pt-BR", "es-ES", "en-US" ],
    "DebugMode": true
  },
  "DbSettings": {
    "ConnectionString": "mongodb://user:pass@localhost:27017/DatabaseName",
    "Name": "DatabaseName"
  },
  "LogSettings": {
    "TitlePrefix": "[{Application}] ",
    "JsonBlacklist": [ "*password", "*card.number", "*creditcardnumber", "*cvv" ],
    "SeqOptions": {
      "Enabled": true,
      "MinimumLevel": "Verbose",
      "Url": "http://localhost:5341",
      "ApiKey": "XXXX"
    },
    "SplunkOptions": {
      "Enabled": false,
      "MinimumLevel": "Verbose",
      "Url": "http://localhost:8088/services/collector",
      "Token": "XXXX",
      "Index": "my.index",
      "Application": "MyApp",
      "ProcessName": "MyApp.Domain.App",
      "Company": "MyApp",
      "ProductVersion": "1.0.0",
      "SourceType": "_json"
    }
  },
  "DocsSettings": {
    "Enabled": true,
    "Title": "MyApp API Reference",
    "TermsOfService": "https://myapp.com/terms-of-service",
    "Description": "MyApp Main API",
    "AuthorName": "Thiago Barradas",
    "AuthorEmail": "thbarradas@gmail.com",
    "BasicAuthUser": "user",
    "BasicAuthPass": "pass"
  }
}

```

## Controller

:construction:

## Models / Validation

:construction:

## Documentation

:construction:

## Install via NuGet

```
PM> Install-Package Nancy.Scaffolding
PM> Install-Package Nancy -Version 2.0.0-clinteastwood
PM> Install-Package Nancy.Serialization.JsonNet -Version 2.0.0-clinteastwood
```

## How can I contribute?
Please, refer to [CONTRIBUTING](.github/CONTRIBUTING.md)

## Found something strange or need a new feature?
Open a new Issue following our issue template [ISSUE_TEMPLATE](.github/ISSUE_TEMPLATE.md)

## Changelog
See in [nuget version history](https://www.nuget.org/packages/Nancy.Scaffolding)

## Did you like it? Please, make a donate :)

if you liked this project, please make a contribution and help to keep this and other initiatives, send me some Satochis.

BTC Wallet: `1G535x1rYdMo9CNdTGK3eG6XJddBHdaqfX`

![1G535x1rYdMo9CNdTGK3eG6XJddBHdaqfX](https://i.imgur.com/mN7ueoE.png)
