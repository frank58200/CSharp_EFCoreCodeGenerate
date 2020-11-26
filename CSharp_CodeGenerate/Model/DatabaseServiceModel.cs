using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_CodeGenerate
{
    public class DatabaseServiceModel: DatabaseModel
    {
        public DatabaseServiceModel()
        {
        }

        

        public DatabaseServiceModel(string className, string dbName, List<string> serviceUsingList, List<DatabaseModel> relationDbList= null, string serviceName="") : base(className, dbName)
        {
            ServiceUsingList = serviceUsingList;
            RelationDbList = relationDbList;
            if(string.IsNullOrWhiteSpace(serviceName))
            {
                ServiceName = className;
            }
        }      

        public string ServiceName { get; set; }
        public List<string> ServiceUsingList { get; set; }

        public List<DatabaseModel> RelationDbList { get; set; }

        
    }

    public class DatabaseModel
    {
        public DatabaseModel()
        {
        }

        public DatabaseModel(string className, string dbName)
        {
            ClassName = className;
            DbName = dbName;
        }

        public string ClassName { get; set; }
        public string DbName { get; set; }
    }
}
