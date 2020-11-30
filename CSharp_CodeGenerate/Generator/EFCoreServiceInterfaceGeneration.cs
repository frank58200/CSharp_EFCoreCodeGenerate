using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;
using System.Collections.Generic;
using Pluralize.NET;
//using Microsoft.EntityFrameworkCore.Design;

namespace CSharp_CodeGenerate
{
    public static class EFCoreServiceInterfaceGeneration
    {

        public static SyntaxNode CreatEFCoreDefaultInterfaceService(DatabaseServiceModel model, GeneratorModel generator)
        {
            IPluralize pluralizer = new Pluralizer();
            string namespace_string = generator.NamespaceString;

            List<String> usingStrings = model.ServiceUsingList;
            string serviceName = model.ServiceName;

            string dbName = generator.ApplicationDbName;

            var EFCoreString = new StringBuilder();
            if (usingStrings.Count > 0)
            {
                foreach (var usingstring in usingStrings)
                {
                    EFCoreString.Append($@"using {usingstring};");
                }
            }
            EFCoreString.Append($@"


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace {namespace_string}.Service.Factory
{{
    public interface I{serviceName}Service
    {{
        
        
        

        Task<{model.ClassName}> Get{serviceName}Async(Guid id);
       

        Task<List<{model.ClassName}>> Get{pluralizer.Pluralize(serviceName)}Async(string query);
       
");
            if (model.RelationDbList.Count > 0)
            {
                foreach (var item in model.RelationDbList)
                {
                    EFCoreString.Append($@"
          Task<List<{model.ClassName}>> Get{pluralizer.Pluralize(serviceName)}From{item.RelationItemName}IdAsync(Guid id);
        
");
                }
            }

            EFCoreString.Append($@"
         Task<int> Update{serviceName}Async({model.ClassName} {model.ClassName.ToLower()});
       
        Task<int> Delete{serviceName}Async(params {model.ClassName}[] {model.DbName.ToLower()});
        
    }}
}}");
            var newNode = SyntaxFactory.ParseSyntaxTree(EFCoreString.ToString()).GetRoot().NormalizeWhitespace();
            return newNode;
        }
    }
}