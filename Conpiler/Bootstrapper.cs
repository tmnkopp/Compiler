using SOM.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOM.FileSys;
namespace Compiler
{
    public static class Bootstrapper
    {
        public static void Run() {

            SOM.FileSys.Utils.DirectoryCreator(AppSettings.SourceDir, AppSettings.BasePath);
            string filelist = 
                $"{AppSettings.SourceDir}_input.txt\n" + 
                $"{AppSettings.SourceDir}_output.txt\n" ;

            SOM.FileSys.Utils.GenerateFiles(filelist);
            FileWriter w = new FileWriter($"{AppSettings.SourceDir}\\_input.txt");  
            w.Write($"[ModelCompile -Clients -PropFormatter]", true);
 
        }
    }
}
