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
                $"{AppSettings.SourceDir}_in.txt\n" + 
                $"{AppSettings.SourceDir}_out.txt";

            SOM.FileSys.Utils.GenerateFiles(filelist);
            FileWriter w = new FileWriter($"{AppSettings.SourceDir}\\_class.txt");
            w.Write($"public class [entity] \n{{\n\t[entity]() {{ \n\t}} \n\t[fields] \n}}", true); 
            w = new FileWriter($"{AppSettings.SourceDir}\\_form.txt");
            w.Write($"\n[fields]\n", true);
        }
    }
}
