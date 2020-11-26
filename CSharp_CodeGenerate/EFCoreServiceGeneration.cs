using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;
using System.Collections.Generic;

namespace CSharp_CodeGenerate
{
    public static class EFCoreServiceGeneration
    {
        public static SyntaxNode CreatEFCoreDefaultService(string namespace_string, DbGenerationModel dbModel, List<String> usingStrings, string serviceName, string dataBaseString, string dbName)
        {

            var EFCoreString = new StringBuilder();
            if (usingStrings.Count > 0)
            {
                foreach (var usingstring in usingStrings)
                {
                    EFCoreString.Append($@"using {usingstring};");
                }
            }
            EFCoreString.Append($@"
using {dataBaseString};
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

        public async Task<{dbModel.ClassName}> Get{dbModel.ClassName}Async(Guid id)
        {{
            var result = await DbContext.{dbModel.DbName}.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            return result;
        }}

        public async Task<List<{dbModel.ClassName}>> Get{dbModel.DbName}Async(string query)
        {{
            var result = await DbContext.{dbModel.DbName}.AsNoTracking().ToListAsync();
            //if(!String.IsNullOrWhiteSpace(query))
            //{{
            //    var param = query.Split("","");
            //    result.Where(b=>b.)
            //}}
            return result;
        }}


        public async Task<int> Update{dbModel.ClassName}Async({dbModel.ClassName} {dbModel.ClassName.ToLower()})
        {{
            var item = await DbContext.{dbModel.DbName}.FirstOrDefaultAsync(b => b.Id == {dbModel.ClassName.ToLower()}.Id);
            if (item == null)
            {{
                DbContext.{dbModel.DbName}.Add({dbModel.ClassName.ToLower()});
            }}
            else
            {{
                DbContext.Entry(item).CurrentValues.SetValues({dbModel.ClassName.ToLower()});
            }}
            return await DbContext.SaveChangesAsync();
        }}

        public async Task<int> Delete{dbModel.DbName}Async(params {dbModel.ClassName}[] {dbModel.DbName.ToLower()})
        {{
            var removelist = new List<{dbModel.ClassName}>();
            foreach (var item in {dbModel.DbName.ToLower()})
            {{
                var Exist = await DbContext.{dbModel.DbName}.FirstOrDefaultAsync(b => b.Id == item.Id);
                if (Exist != null)
                {{
                    removelist.Add(Exist);
                }}


            }}
            DbContext.{dbModel.DbName}.RemoveRange(removelist);
            return await DbContext.SaveChangesAsync();
        }}
    }}
}}");
            var newNode = SyntaxFactory.ParseSyntaxTree(EFCoreString.ToString()).GetRoot().NormalizeWhitespace();
            return newNode;
        }
    }
}