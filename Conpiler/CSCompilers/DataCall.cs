using SOM.Procedures;
using SOM.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOM.IO;
using SOM.Parsers;
using Compiler.Procedures;
using SOM.Extentions;

namespace Compiler
{
    public static class DataCallCompiler  
    { 
        public static void Run()
        {
            Cache.Write("");
            Dictionary<string, string> kvp = new Dictionary<string, string>();
            kvp.Add("Q4", "Q1");
            kvp.Add("2020_", "2021_");

            SOM.Compilers.SomCompiler compiler = new SOM.Compilers.SomCompiler();
            compiler.CompileMode = CompileMode.Commit;
            compiler.Source = AppSettings.SourceDir + "\\_compile";
            compiler.FilenameCompilers.Add(new KeyValReplacer(kvp));
            //compiler.ContentCompilers.Add(new MetricCommenter());
            compiler.ContentCompilers.Add(new KeyValReplacer(compiler.Source + "\\keyval.sql"));
            compiler.ContentCompilers.Add(new KeyValReplacer(kvp));
             
            compiler.FileFilter = "*aspx*";
            compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\RMA\2021";
            //compiler.Dest = compiler.Source + "\\_compiled";
            compiler.Compile(); 
            Cache.CacheEdit(); 
        }
    }
}
 

