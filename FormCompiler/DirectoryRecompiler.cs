using SOM.Compilers;
using SOM.Extentions;
using SOM.IO;
using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Compiler
{ 
    public class DirectoryRecompiler : BaseCompiler 
    {
        public DirectoryRecompiler(  )
        { } 
        public override void Compile()
        {
            ProcessDirectory(Source);
        }
        public void ProcessDirectory(string targetDirectory)
        { 
            string newDir = targetDirectory.Replace(Source, Dest); 
            foreach (IInterpreter proc in FilenameCompilers)
                newDir = proc.Interpret(newDir).RemoveWhiteAndBreaks();

            DirectoryInfo DI = new DirectoryInfo(newDir);
            if (CompileMode == CompileMode.Debug)
                Console.WriteLine(newDir);

            if (!DI.Exists && (CompileMode == CompileMode.Commit))
            {
                DI.Create();
            }
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);
             
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        } 
        public void ProcessFile(string path)
        {
            FileReader r = new FileReader($"{path}");
            string content = r.Read();
            foreach (IInterpreter proc in ContentCompilers)
                content = proc.Interpret(content);
             
            string newFileName = path.Replace(Source, Dest);
            foreach (IInterpreter proc in FilenameCompilers)
                newFileName = proc.Interpret(newFileName).RemoveWhiteAndBreaks();

            if (CompileMode == CompileMode.Commit)
            {
                FileWriter w = new FileWriter($"{newFileName}");
                w.Write($"{newFileName}", true);
            }
            if (CompileMode == CompileMode.Debug)
                Console.WriteLine($"{newFileName}\n{content}\n");

        } 
    }  
}
