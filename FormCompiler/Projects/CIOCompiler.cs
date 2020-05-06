using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
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
             
            Source = @"C:\temp\templates\RMA\";
            Dest = @"C:\temp\templates\RMA\compiled\";
            FilenameCompilers = new List<ICompiler>() {
                new JsonCompile("{  \"Q2\":\"Q3\"  }")
            };
            ContentCompilers = new List<ICompiler>()  {
                new SqlKeyValCompile(@"C:\temp\templates\RMA\dbupdate.sql")
            }; 
        }
        public void RMABuild(CompileMode compileMode)
        {
            CompilationBuilder<CIOCompiler> compiler = new CompilationBuilder<CIOCompiler>();
            compiler.Init()
                .CompileMode(compileMode)  
                .FileFilter("*.asp*").Compile()
                ;
        }
        public void CIOBuild() {
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


