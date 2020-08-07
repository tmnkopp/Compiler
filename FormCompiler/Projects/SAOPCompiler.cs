using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Compiler
{
    public class SAOPCompiler : BaseCompiler
    {
        public static string ProdPath = @"D:\\dev\\CyberScope\\CyberScopeBranch\\CSwebdev\\";
        public static string CodePath = $"{ProdPath}\\code\\CyberScope\\FismaForms\\2020\\";
        public static string DBPath = $"{ProdPath}\\database\\";
        public static string SprocPath = $"{ProdPath}\\database\\sprocs\\";
        public static string TempPath = $"C:\\temp\\templates\\SAOP\\$compiled\\";
       
        public SAOPCompiler()
        { 
            Source = @"C:\temp\templates\SAOP\";
            Dest = @"C:\temp\templates\SAOP\$compiled\";
            FilenameCompilers = new List<ICompiler>() {
                new JsonCompile("{  \"2019\":\"2020\"  }")
            };
            ContentCompilers = new List<ICompiler>() {
                    new PK_QuestionInject() 
                    ,new SqlKeyValCompile(@"C:\temp\templates\SAOP\dbupdate.sql") 
            }; 
        }
        public void Run() {
            CompileMode = CompileMode.Commit;
            FileFilter = "*.aspx*";
            Dest = SAOPCompiler.CodePath;
            this.Compile();

            FileFilter = "DB_Update*.sql";
            Dest = SAOPCompiler.DBPath;
            this.Compile(); 

            FileFilter = "frmval_*.sql";
            Dest = SAOPCompiler.SprocPath;
            this.Compile();


        }
    }
}


