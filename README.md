# CSharp_EFCoreCodeGenerate

## Create the cs file of ApplicationDBContext and EFCore Service.
Using the Library can generate the DbContext and the service for DI.

## Creating the ApplicationDbContext and the seperate Serivce
```csharp

var applicationDbName = "ApplicationDbContext";
var namespace_string = "WebApi";

//Adding the namespace for the file which need that namesapce
var usingList = new List<string>();
usingList.Add(new string("WebApiModel"));
usingList.Add(new string("WebApi.Database"));

// Creating the Service Model
DatabaseServiceModel databaseService = new DatabaseServiceModel("DbGenerationModel", "MyDbs", usingList);

// Creating the List of the Services
var db_List = new List<DatabaseServiceModel>();
db_List.Add(databaseService);

// Creating the Application and Services Model
GeneratorModel generator = new GeneratorModel(applicationDbName, namespace_string, db_List);

//Running the generator to create the file.
Generation generation = new Generation();
generation.Execute(generator,false);
````
Then, you will get the files "ApplicationDbContext.cs" and "DbGenerationModelService.cs"

For the services that you want to put it in the same service name

You need to change to
```csharp
// Add the Main Service to contain the DbContext and not contain the other services
// In this model, only the usingList and folderGroupName will be use
var uniformModel = new DatabaseServiceModel(nameof(DbGenerationModel), pluralizer.Pluralize(nameof(DbGenerationModel)), usingList, folderGroupName: "DbGenerationModel");
// if you input the string DbGeneration in that position, you will get the services name as DbGeneration
GeneratorModel generator = new GeneratorModel(applicationDbName, namespace_string, db_List,"DbGeneration", uniformModel);
Generation generation = new Generation();
generation.Execute(generator,true);
```
