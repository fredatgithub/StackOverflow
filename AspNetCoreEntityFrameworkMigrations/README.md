
Enter the demo directory, restore, and build.
```
cd AspNetCoreEntityFrameworkMigrations
dotnet restore
dotnet build */**/project.json
```

Run the migrations and update the database.
```
cd Datalayer.Migrations
dotnet ef migrations add initial
dotnet ef database update
```

Test the migration (optional - this is NOT necessary for migrations)
```
dotnet run
```