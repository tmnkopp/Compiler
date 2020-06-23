using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Compiler
{
    public class IGCompiler : BaseCompiler
    {
        public static string ProdPath = @"D:\\dev\\CyberScope\\25\\CSwebdev\\";
        public static string CodePath = $"{ProdPath}\\code\\CyberScope\\FismaForms\\2019\\";
        public static string DBPath = $"{ProdPath}\\database\\";
        public static string SprocPath = $"{ProdPath}\\database\\sprocs\\";
        public IGCompiler()
        {
            CompileMode = CompileMode.Commit;
            Source = CodePath;
            Dest = @"C:\temp\templates\IG2020\compiled"; // @"C:\temp\templates\IG2020\compiled\";
            FileFilter = "*2019_A_IG*";
            FilenameCompilers = new List<ICompiler>() {
                new JsonCompile("{  \"2019\":\"2020\"  }")
            };
            ContentCompilers = new List<ICompiler>();
        } 
        public void Run() {
            ContentCompilers.Add(new SqlKeyValCompile(@"C:\temp\templates\IG2020\dbupdate.sql")); 
            this.Compile();
        }
    }
}


