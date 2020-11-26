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
            GenDB();
            GenService();
        }

        private static void GenDB()
        {
            var dbfile = File.CreateText("ApplicationDbContext.cs");
            var list = new List<DbGenerationModel>();
            list.Add(new DbGenerationModel() { ClassName = "DbGenerationModel", DbName = "MyDbs" });
            list.Add(new DbGenerationModel() { ClassName = "DbGenerationModel", DbName = "MyDbs2" });
            var dbgeneration = DbGeneration.GenerateDb("CodeGenerationTest", list, "ApplicationDbContext");
            dbgeneration.WriteTo(dbfile);
            dbfile.Close();
            
            
        }

        private static void GenService()
        {
            var genName = "GeometricVertex";
            var serviceFile = File.CreateText($"{genName}Service.cs");
            var dbModel = new DbGenerationModel() { ClassName = "GeometricVertex", DbName = "GeometricVertices" };
            var usingList = new List<string>();
            usingList.Add(new string("AutoAROjectModel"));
            usingList.Add(new string("QChoice_AutoAR_Api.Database"));
            var serviceGeneration = EFCoreServiceGeneration.CreatEFCoreDefaultService("QChoice_AutoAR_Api", dbModel, usingList, genName, "ApplicationDbContext", dbModel.DbName);
            serviceGeneration.WriteTo(serviceFile);
            serviceFile.Close();
        }
    }
}
