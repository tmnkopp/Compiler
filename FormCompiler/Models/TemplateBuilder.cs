using Compiler.Models;
using SOM.Procedures;
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
    public class TemplateWriter
    {
        private List<TemplateFile> _TemplateFiles = new List<TemplateFile>(); 
        private bool _CreateDirectories = false;
        public bool CreateDirectories
        {
            get { return _CreateDirectories; }
            set { _CreateDirectories = value; }
        } 
        public TemplateWriter(List<TemplateFile> TemplateFiles)
        {
            _TemplateFiles = TemplateFiles;
        }
        public TemplateWriter(List<TemplateFile> TemplateFiles, bool CreateDirectories)
        {
            _TemplateFiles = TemplateFiles;
            _CreateDirectories = CreateDirectories;
        }
        public void Build()
        { 
            foreach (TemplateFile TemplateFile in _TemplateFiles)
            {
                if (CreateDirectories)
                    SOM.FileSys.Utils.DirectoryCreator(TemplateFile.Path, AppSettings.BasePath);
                WriteTemplate(TemplateFile); 
            }
        } 
        public void WriteTemplate(TemplateFile templateFile)
        {
            string content = templateFile.Content;
            foreach (ICompiler proc in templateFile.ContentCompilers)
                content = proc.Compile(content);
         
            string path = templateFile.Path;
            FileWriter w = new FileWriter($"{path}");
            w.Write($"{content}", true); 
        }
    }
}
