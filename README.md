# TwitterTopicModeling
Twitter topic modeling API 


# Requremnts 
Here are the Requirment to run Twitter Topic Modeling API
- [postgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
- vscode
- .net 5 
- enitity framework CLI


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
- [Microsoft EntityFrameworkCore] ()
- [Flurl] (https://github.com/tmenier/Flurl)
- [Json.net] (https://github.com/JamesNK/Newtonsoft.Json)
- [Enitiy FrameWork posgeSQL libary] ()
