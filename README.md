# TwitterTopicModeling
Twitter topic modeling API 


# Requirements
Here are the Requirment to run Twitter Topic Modeling API
- [postgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
- [vscode](https://code.visualstudio.com/download)
- [.net 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [enitity framework CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
- [R](https://www.r-project.org/)


# Configuration 
How to configure appsettings.Development.json file
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
    "BearerToken": "Twitter API Bearer Token"
  },

    "ConnectionStrings": {
      "DefaultConnection": "Your Database Connection String"
  },

  "Rscript": "Path to r file in repo",
  "exeRpath": "PATH TO Rscript.exe on your machine"
}

```

How to configure R
- go to Rprofile.site in the r files after downloading and set the CRAN mirror to http://cran.r-project.org

How to use Dependency Injection in Entity Framework Core 
- https://hackernoon.com/asp-net-core-how-to-use-dependency-injection-in-entity-framework-core-4388fc5c148b 

How to use serilization and deserialization of json in .net 
- https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0#how-to-write-net-objects-as-json-serialize

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
- [Csvhelper](https://joshclose.github.io/CsvHelper/)
- [Temp Directory package](https://gist.github.com/JoeHartzell/ab6ebd4af690c79e84c728f5da367dcc)

# Commands 
run any of the following in the terminal of vscode in the directory of API project
- `dotnet run`
- `dotnet build`
- `dotnet watch run`
