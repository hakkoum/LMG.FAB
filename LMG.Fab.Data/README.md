# formation

Entity Framework Core: Getting Started
by Julie Lerman

# installation

https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql


Dans le fichier csproj :
```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <RuntimeFrameworkVersion>2.0.3</RuntimeFrameworkVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.0.*" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.*" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="2.0.*" />
     <PackageReference Include="Microsoft.Extensions.Configuration" Version="2.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="2.0.0" />
  </ItemGroup>

  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
  </ItemGroup>

</Project>
```


En développements :
```
# création de la migration
dotnet ef migrations add InitialCreate

# liste des migrations
dotnet ef migrations list

# application de la migration dans la base (ddl)
dotnet ef database update

```