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
        public void Execute(GeneratorModel generator, bool useUniform)
        {
            List<string> ServiceDIString = new List<string>();
            GenDB(generator);
            if (!useUniform)
            {
                foreach (var item in generator.Db_List)
                {
                    GenService(generator, item);
                    GenServiceInterface(generator, item);
                    ServiceDIString.Add(GetDIString(item.ServiceName));
                }
            }
            else
            {
                GenUniformCenterService(generator, generator.UniformModel);
                foreach (var item in generator.Db_List)
                {
                    GenUniformService(generator, item);
                } 
            }
            bool IsWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (IsWindows)
            {
                FolderSymbol = "\\";
            }
            Console.WriteLine($"Current Folder : {Directory.GetCurrentDirectory()}");
            Directory.CreateDirectory("Snippest");
            File.AppendAllLines($"Snippest{FolderSymbol}Startup.txt", ServiceDIString);






        }
        private void GenDB(GeneratorModel generator)
        {
            string FolderPath = $"CreatedFile{FolderSymbol}Database{FolderSymbol}";
            Directory.CreateDirectory(FolderPath);
            var dbfile = File.CreateText($"{FolderPath}{generator.ApplicationDbName}.cs");
            var dbgeneration = DbGeneration.GenerateDb(generator);
            dbgeneration.WriteTo(dbfile);
            dbfile.Close();

        }

        private void GenService(GeneratorModel generator, DatabaseServiceModel databaseService)
        {
            string folderpath = $"CreatedFile{FolderSymbol}Service{FolderSymbol}";
            if (!string.IsNullOrWhiteSpace(databaseService.FolderGroupName))
            {
                folderpath += $"{databaseService.FolderGroupName}{FolderSymbol}";
            }

            Directory.CreateDirectory(folderpath);

            var serviceFile = File.CreateText($"{folderpath}{databaseService.ServiceName}Service.cs");
            var serviceGeneration = EFCoreServiceGeneration.CreatEFCoreDefaultService(databaseService, generator);
            serviceGeneration.WriteTo(serviceFile);
            serviceFile.Close();
        }

        private void GenServiceInterface(GeneratorModel generator, DatabaseServiceModel databaseService)
        {
            string folderpath = $"CreatedFile{FolderSymbol}Service{FolderSymbol}Factory{FolderSymbol}";
            if (!string.IsNullOrWhiteSpace(databaseService.FolderGroupName))
            {
                folderpath += $"{databaseService.FolderGroupName}{FolderSymbol}";
            }

            Directory.CreateDirectory(folderpath);

            var serviceFile = File.CreateText($"{folderpath}I{databaseService.ServiceName}Service.cs");
            var serviceGeneration = EFCoreServiceInterfaceGeneration.CreatEFCoreDefaultInterfaceService(databaseService, generator);
            serviceGeneration.WriteTo(serviceFile);
            serviceFile.Close();
        }

        private void GenUniformService(GeneratorModel generator, DatabaseServiceModel databaseService)
        {
            string folderpath = $"CreatedFile{FolderSymbol}Service{FolderSymbol}";
            if (!string.IsNullOrWhiteSpace(databaseService.FolderGroupName))
            {
                folderpath += $"{databaseService.FolderGroupName}{FolderSymbol}";
            }

            Directory.CreateDirectory(folderpath);

            var serviceFile = File.CreateText($"{folderpath}{generator.UniformServiceName}Service.{databaseService.ServiceName}.cs");
            var serviceGeneration = EFCoreServiceInOneGeneration.CreatEFCoreDefaultService(databaseService, generator);
            serviceGeneration.WriteTo(serviceFile);
            serviceFile.Close();
        }

        private void GenUniformCenterService(GeneratorModel generator, DatabaseServiceModel databaseService)
        {
            string folderpath = $"CreatedFile{FolderSymbol}Service{FolderSymbol}";
            if (!string.IsNullOrWhiteSpace(databaseService.FolderGroupName))
            {
                folderpath += $"{databaseService.FolderGroupName}{FolderSymbol}";
            }

            Directory.CreateDirectory(folderpath);

            var serviceFile = File.CreateText($"{folderpath}{generator.UniformServiceName}Service.cs");
            var serviceGeneration = EFCoreServiceInOneGeneration.CreatEFCoreCenterService(databaseService, generator);
            serviceGeneration.WriteTo(serviceFile);
            serviceFile.Close();
        }

        private string GetDIString(string serviceName)
        {
            return $"services.AddScoped<I{serviceName}Service, {serviceName}Service>();";
        }
    }


}
