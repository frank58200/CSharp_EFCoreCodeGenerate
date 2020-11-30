using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_CodeGenerate
{
    public class GeneratorModel
    {
        public GeneratorModel()
        {
        }

        public GeneratorModel(string applicationDbName, string namespaceString, List<DatabaseServiceModel> db_List)
        {
            ApplicationDbName = applicationDbName;
            NamespaceString = namespaceString;
            Db_List = db_List;
        }

        public GeneratorModel(string applicationDbName, string namespaceString, List<DatabaseServiceModel> db_List, string uniformServiceName, DatabaseServiceModel uniformModel) : this(applicationDbName, namespaceString, db_List)
        {
            UniformServiceName = uniformServiceName;
            UniformModel = uniformModel;
        }

        public string ApplicationDbName { get; set; }
        public string NamespaceString { get; set; }
        public List<DatabaseServiceModel> Db_List { get; set; }

        public string UniformServiceName { get; set; }

        public DatabaseServiceModel UniformModel { get; set; }
            
    }
}
