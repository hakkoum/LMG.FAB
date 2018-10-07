# Formation

- Using ASP.NET Core to Build Single-page Applications
by Ajden Towfeek
- Building a Web App with ASP.NET Core, MVC, Entity Framework Core, Bootstrap, and Angular
by Shawn Wildermuth


# Prerequis 

- Installer npm


# Installation

Installer les templates SPA :

```
dotnet new --install Microsoft.AspNetCore.SpaTemplates::*
```

Environnement : 
Windows : setx ASPNETCORE_ENVIRONMENT "Development"
Linux :   export ASPNETCORE_ENVIRONMENT=Development
ou bien 
fichier hosting.js :
```
{
    "server.urls": "http://localhost:5000",
    "environment": "Development"
}
```
puis dans Program.cs :
```
        ...
        public static IWebHost BuildWebHost(string[] args) {

            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("hosting.json", optional: true)
             .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }
        ...
```

# Productivité

## Recompilation automatique :
(https://github.com/aspnet/DotNetTools)

Mettre ceci dans le fichier csproj :
```
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
  </ItemGroup>
```

Puis lancer `dotnet restore`

Puis lancer le serveur avec `dotnet watch run`


## LESS

dans la console :
```
npm install --save sass-loader sass node-sass
```

dans webpack.config.js :
```
        module: {
            rules: [
                { test: /\.ts$/, include: /ClientApp/, use: isDevBuild ? ['awesome-typescript-loader?silent=true', 'angular2-template-loader'] : '@ngtools/webpack' },
                { test: /\.html$/, use: 'html-loader?minimize=false' },
                { test: /\.css$/, use: [ 'to-string-loader', isDevBuild ? 'css-loader' : 'css-loader?minimize' ] },
                { test: /\.sass$/, include: /ClientApp/, loader:'raw-loader!sass-loader'},
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' }
            ]
        },
```

# Ajout de librairies tierces

Installer Webpack :

```
npm install -g webpack
```

Puis recobnstruire le fichier vendor.js avec la commande suivante :
```
webpack --config webpack.config.vendor.js
```

# Déploiement

```
dotnet publish -c Release
```

Le résultat est dans `bin\Release\netcoreapp2.0\publish`.
