using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOM.Compilers; 
namespace Compiler
{
    public class CIOCompiler : BaseCompiler
    {
        public static string ProdPath = @"D:\\dev\\CyberScope\\CyberScopeBranch\\CSwebdev\\";
        public static string CodePath = $"{ProdPath}\\code\\CyberScope\\FismaForms\\2020\\";
        public static string DBPath = $"{ProdPath}\\database\\";
        public static string SprocPath = $"{ProdPath}\\database\\sprocs\\";
        public CIOCompiler()
        {
            Source = @"C:\temp\templates\CIO\";
            Dest = @"C:\temp\templates\CIO\$compiled\";
            FilenameCompilation = new List<IProcedure>() {
                new JsonReplace("{ \"Q2\":\"Q3\" , \"7.22\":\"7.24\" }")
            };
            ContentCompilation = new List<IProcedure>()  {
                new SqlKeyValCompile(@"C:\temp\templates\CIO\dbupdate.sql")
            };

        }
        public void Build() {
            CompilationBuilder<CIOCompiler> compiler = new CompilationBuilder<CIOCompiler>();
            compiler.Init()
                .CompileMode(CompileMode.Commit) 
                .Dest(CIOCompiler.DBPath)
                .FileFilter("*DB_Update*.sql").Compile()
                .Dest(CIOCompiler.SprocPath)
                .FileFilter("*frmval_*.sql").Compile()
                .Dest(CIOCompiler.CodePath)
                .FileFilter("*.asp*").Compile()
                ; 
        }
    }
}


