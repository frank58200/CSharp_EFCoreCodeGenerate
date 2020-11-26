using CSharp_CodeGenerate;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeGenerationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicationDbName = "ApplicationDbContext";
            var namespace_string = "QChoice_AutoAR_Api";
            var usingList = new List<string>();
            usingList.Add(new string("AutoAROjectModel"));
            usingList.Add(new string("QChoice_AutoAR_Api.Database"));
            DatabaseServiceModel databaseService = new DatabaseServiceModel("DbGenerationModel", "MyDbs", usingList);
            var db_List = new List<DatabaseServiceModel>();
            db_List.Add(databaseService);
            GeneratorModel generator = new GeneratorModel(applicationDbName, namespace_string, db_List);
            GenDB(generator);
            foreach (var item in db_List)
            {
                GenService(generator, item);
            }
        }

        private static void GenDB(GeneratorModel generator)
        {
            var dbfile = File.CreateText($"{generator.ApplicationDbName}.cs");
            var dbgeneration = DbGeneration.GenerateDb(generator);
            dbgeneration.WriteTo(dbfile);
            dbfile.Close();

        }

        private static void GenService(GeneratorModel generator, DatabaseServiceModel databaseService)
        {
            var serviceFile = File.CreateText($"{databaseService.ServiceName}Service.cs");
            var serviceGeneration = EFCoreServiceGeneration.CreatEFCoreDefaultService(databaseService, generator);
            serviceGeneration.WriteTo(serviceFile);
            serviceFile.Close();
        }
    }
}
