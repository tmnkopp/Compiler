using SOM.Procedures;
using SOM.Compilers;
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
        public static string TempPath = $"C:\\temp\\templates\\CIO\\$compiled\\";
        public CIOCompiler()
        { 
            Source = @"C:\temp\templates\CIO\";
            Dest = @"C:\temp\templates\CIO\$compiled\";
            FilenameCompilers = new List<IInterpreter>() {
                new KeyValInterpreter("{  \"Q3\":\"Q4\"  }")
            };
            ContentCompilers = new List<IInterpreter>()  {
                new SqlKeyValInterpreter(@"C:\temp\templates\CIO\dbupdate.sql")
            }; 
        }
        public void Run() {
            CompileMode = CompileMode.Debug;
            FileFilter = "DB_Update*.sql";
            this.Compile();
            FileFilter = "frmval_*.sql";
            this.Compile();
            FileFilter = "*.asp*";
            this.Compile(); 
        }
    }
}


