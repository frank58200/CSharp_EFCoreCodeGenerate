using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_CodeGenerate.Generator
{
    public class Generation
    {
        private string FolderSymbol = "/";
        public void Execute(GeneratorModel generator)
        {
            GenDB(generator);
            foreach (var item in generator.Db_List)
            {
                GenService(generator, item);
            }
            bool IsWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if(IsWindows)
            {
                FolderSymbol = "\\";
            }
            Console.WriteLine($"Current Folder : {Directory.GetCurrentDirectory()}");
            
            
            
        }
        private  void GenDB(GeneratorModel generator)
        {
            string FolderPath = $"CreatedFile{FolderSymbol}Database{FolderSymbol}";
            Directory.CreateDirectory(FolderPath);
            var dbfile = File.CreateText($"{FolderPath}{generator.ApplicationDbName}.cs");
            var dbgeneration = DbGeneration.GenerateDb(generator);
            dbgeneration.WriteTo(dbfile);
            dbfile.Close();

        }

        private  void GenService(GeneratorModel generator, DatabaseServiceModel databaseService)
        {
            string folderpath = $"CreatedFile{FolderSymbol}Service{FolderSymbol}";
            if (string.IsNullOrWhiteSpace(databaseService.FolderGroupName))
            {
                Directory.CreateDirectory(folderpath);
            }
            else
            {
                folderpath += $"{databaseService.FolderGroupName}{FolderSymbol}";
            }

            var serviceFile = File.CreateText($"{folderpath}{databaseService.ServiceName}Service.cs");
            var serviceGeneration = EFCoreServiceGeneration.CreatEFCoreDefaultService(databaseService, generator);
            serviceGeneration.WriteTo(serviceFile);
            serviceFile.Close();
        }
    }


}
