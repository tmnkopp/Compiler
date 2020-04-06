using Compiler.Models;
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
    public class DirectoryBuilder
    {
        private List<TemplateFile> _TemplateFiles = new List<TemplateFile>();
        private List<IProcedure> _ContentCompilers;
        public DirectoryBuilder(List<TemplateFile> TemplateFiles)
        {
            _TemplateFiles = TemplateFiles;
        }
        public void Build()
        {
            foreach (TemplateFile TemplateFile in _TemplateFiles)
            {
                
                if (!TemplateFile.Content.Contains("."))
                    ProcessDirectory("");
                else
                    ProcessFile(TemplateFile);
            }
        }
        public void ProcessDirectory(string targetDirectory)
        {
            DirectoryInfo DI = new DirectoryInfo(targetDirectory);
            if (!DI.Exists)
                DI.Create();
        }
        public void ProcessFile(TemplateFile templateFile)
        {
            string content = templateFile.Content;
            string path = templateFile.Path;
            foreach (IProcedure proc in templateFile.ContentCompilers)
                content = proc.Execute(content);
         
            FileWriter w = new FileWriter($"{path}");
            w.Write($"{path}", true);
    
        }
    }
}
