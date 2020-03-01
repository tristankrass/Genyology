### Running docker 🐳

```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Test@123' --rm --name mssql --publish 1433:1433 mcr.microsoft.com/mssql/server:2019-CU1-ubuntu-16.04
```

### Installing dependencies

```
cd WebApp
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

```
cd DAL.App.EF
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

```

### Running migrations
These commands should be ran at the top level of your solution.
```
dotnet ef migrations add InitialDBDesign --project DAL.App.EF --startup-project WebApp
```

### Validating model with razorpages
```
dotnet aspnet-codegenerator razorpage -m Family -dc ApplicationDbContext -udl -outDir Pages/Families --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m Person -dc ApplicationDbContext -udl -outDir Pages/Persons --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m Relationship -dc ApplicationDbContext -udl -outDir Pages/Relationships --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m RelationshipType -dc ApplicationDbContext -udl -outDir Pages/RelationshipTypes --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m Role -dc ApplicationDbContext -udl -outDir Pages/Roles --referenceScriptLibraries -f
```
- [ ] Family should have many people.
- [ ] Relationship should have 1 or 2 families.

### Data model

![Data Model](data_model.png)