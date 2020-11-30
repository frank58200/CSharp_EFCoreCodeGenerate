using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_CodeGenerate
{
    public class DatabaseServiceModel : DatabaseModel
    {
        public DatabaseServiceModel()
        {
        }



        public DatabaseServiceModel(string className, string dbName, List<string> serviceUsingList, List<RelationModel> relationDbList = null, string serviceName = "", string folderGroupName = "") : base(className, dbName)
        {
            ServiceUsingList = serviceUsingList;
            if (relationDbList == null)
            {
                RelationDbList = new List<RelationModel>();
            }
            else
            {

                RelationDbList = relationDbList;
            }
            FolderGroupName = folderGroupName;
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                ServiceName = className;
            }
            else
            {
                ServiceName = serviceName;
            }

        }

        public string ServiceName { get; set; }

        public string FolderGroupName { get; set; }
        public List<string> ServiceUsingList { get; set; }

        public List<RelationModel> RelationDbList { get; set; }


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

    public class RelationModel
    {
        public RelationModel()
        {
        }

        public RelationModel(string relationItemName, string memberName)
        {
            RelationItemName = relationItemName;
            MemberName = memberName;
        }

        public string RelationItemName { get; set; }
        public string MemberName { get; set; }
    }
}
