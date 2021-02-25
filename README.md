# TwitterTopicModeling
Twitter topic modeling API 


# Requirements
Here are the Requirment to run Twitter Topic Modeling API
- [postgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
- [vscode](https://code.visualstudio.com/download)
- [.net 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [enitity framework CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)


# Configuration 
How to configure
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },

  "Twitter": {
    "BearerToken": "USE YOUR BEARER TOKEN HERE"
  },

    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=DATABASE NAME ;Username=USERNAME ;Password=Password"
  }
  
}

```

How to use Dependency Injection in Entity Framework Core 
https://hackernoon.com/asp-net-core-how-to-use-dependency-injection-in-entity-framework-core-4388fc5c148b 

## Database
how to run migrations
```powershell
dotnet ef database update
```

# Packages
- [Microsoft EntityFrameworkCore](https://github.com/dotnet/efcore)
- [Flurl](https://github.com/tmenier/Flurl)
- [Json.net](https://github.com/JamesNK/Newtonsoft.Json)
- [Enitiy FrameWork posgeSQL libary](https://www.npgsql.org/)
- [Swagger](https://github.com/swagger-api/swagger-ui)

# Commands 
run any of the following in the terminal of vscode in the directory of API project
- `dotnet run`
- `dotnet build`
- `dotnet watch run`
