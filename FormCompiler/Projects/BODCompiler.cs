using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Compiler
{
    public class BODCompiler : BaseCompiler
    {
        public static string ProdPath = @"D:\\dev\\CyberScope\\CyberScopeBranch\\CSwebdev\\";
        public static string CodePath = $"{ProdPath}\\code\\CyberScope\\FismaForms\\2020\\";
        public static string DBPath = $"{ProdPath}\\database\\";
        public static string SprocPath = $"{ProdPath}\\database\\sprocs\\";
        public static string TempPath = $"C:\\temp\\templates\\HVA1802\\$compiled\\";
        public BODCompiler()
        { 
            Source = @"C:\temp\templates\HVA1802\";
            Dest = @"C:\temp\templates\HVA1802\$compiled\";
            FilenameCompilers = new List<ICompiler>() {
                new JsonCompile("{  \"2019\":\"2020\"  }")
            };
            ContentCompilers = new List<ICompiler>()  {
                new SqlKeyValCompile(@"C:\temp\templates\HVA1802\dbupdate.sql")
            }; 
        }
        public void Run() {
            CompileMode = CompileMode.Commit;
            FileFilter = "DB_Update*.sql";
            //this.Compile();
            FileFilter = "frmval_*.sql";
            //this.Compile();
            FileFilter = "*.asp*";
            this.Compile(); 
        }
    }
}


