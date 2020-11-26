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
    public static class EFCoreServiceGeneration
    {

        public static SyntaxNode CreatEFCoreDefaultService(DatabaseServiceModel model, GeneratorModel generator)
        {
            IPluralize pluralizer = new Pluralizer();
            string namespace_string = generator.NamespaceString;

            List<String> usingStrings = model.ServiceUsingList;
            string serviceName = model.ClassName;

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
using {namespace_string}.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace {namespace_string}.Service
{{
    public class {serviceName}Service
    {{
        private readonly {dbName} DbContext = null;
        public {serviceName}Service({dbName} dbContext)
        {{
            DbContext = dbContext;
        }}

        public async Task<{model.ClassName}> Get{model.ClassName}Async(Guid id)
        {{
            var result = await DbContext.{model.DbName}.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            return result;
        }}

        public async Task<List<{model.ClassName}>> Get{pluralizer.Pluralize(model.ClassName)}Async(string query)
        {{
            var result = await DbContext.{model.DbName}.AsNoTracking().ToListAsync();
            //if(!String.IsNullOrWhiteSpace(query))
            //{{
            //    var param = query.Split("","");
            //    result.Where(b=>b.)
            //}}
            return result;
        }}
");

            EFCoreString.Append($@"
        public async Task<int> Update{model.ClassName}Async({model.ClassName} {model.ClassName.ToLower()})
        {{
            var item = await DbContext.{model.DbName}.FirstOrDefaultAsync(b => b.Id == {model.ClassName.ToLower()}.Id);
            if (item == null)
            {{
                DbContext.{model.DbName}.Add({model.ClassName.ToLower()});
            }}
            else
            {{
                DbContext.Entry(item).CurrentValues.SetValues({model.ClassName.ToLower()});
            }}
            return await DbContext.SaveChangesAsync();
        }}

        public async Task<int> Delete{model.DbName}Async(params {model.ClassName}[] {model.DbName.ToLower()})
        {{
            var removelist = new List<{model.ClassName}>();
            foreach (var item in {model.DbName.ToLower()})
            {{
                var Exist = await DbContext.{model.DbName}.FirstOrDefaultAsync(b => b.Id == item.Id);
                if (Exist != null)
                {{
                    removelist.Add(Exist);
                }}


            }}
            DbContext.{model.DbName}.RemoveRange(removelist);
            return await DbContext.SaveChangesAsync();
        }}
    }}
}}");
            var newNode = SyntaxFactory.ParseSyntaxTree(EFCoreString.ToString()).GetRoot().NormalizeWhitespace();
            return newNode;
        }
    }
}