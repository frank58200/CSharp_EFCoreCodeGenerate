using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_CodeGenerate
{
    public static class DbGeneration
    {
        public static SyntaxNode GenerateDb(string namespace_string, List<DbGenerationModel> dbs, string DbContextName)
        {

            var DefaultDBContextString = new StringBuilder($@"
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace {namespace_string}.Database
            
{{
    public class {DbContextName}: DbContext
    {{
        public {DbContextName}([NotNullAttribute] DbContextOptions options) : base(options)
        {{
            
        }}

        protected {DbContextName}()
        {{

        }}
");
            foreach (var db in dbs)
            {

                DefaultDBContextString.Append($"public DbSet<{db.ClassName}> {db.DbName} {{ get; set; }}");
            }
            DefaultDBContextString.Append(@" 
    } 
}"
);
            var newNode = SyntaxFactory.ParseSyntaxTree(DefaultDBContextString.ToString()).GetRoot().NormalizeWhitespace();
            return newNode;
        }
    }
}
