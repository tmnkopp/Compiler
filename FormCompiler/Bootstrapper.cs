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
                $"{AppSettings.SourceDir}_config.json\n" + 
                $"{AppSettings.SourceDir}_components\\_config.json\n" +
                $"{AppSettings.SourceDir}_services\\_config.json\n";

            SOM.FileSys.Utils.GenerateFiles(filelist);
            FileWriter w = new FileWriter($"{AppSettings.SourceDir}\\_input.txt");  
            w.Write($"[ModelCompile -Clients -PropFormatter]\n[ModelCompile -Clients -NgInput]", true);

            /*
             
            [ModelCompile -Clients -PropFormatter]
             
             ModelCompiler compiler;
             compiler = new ModelCompiler(
                "Clients",
                "Compiler.Formatters.PropFormatter, Compiler");


               content.Replace($"[ModelCompile -{_ModelName} -{_TypeFormatter.GetType().Name}]
             */
        }
    }
}
