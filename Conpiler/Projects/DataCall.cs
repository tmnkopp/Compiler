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
             
            AppSettingsCompiler compiler = new AppSettingsCompiler();
            compiler.CompileMode = CompileMode.Commit;
            compiler.Source = AppSettings.SourceDir + "\\_compile";
            compiler.FilenameCompilers.Add(new KeyValInterpreter(kvp));
            //compiler.ContentCompilers.Add(new MetricCommenter());
            compiler.ContentCompilers.Add(new SqlKeyValInterpreter(compiler.Source + "\\keyval.sql"));
            compiler.ContentCompilers.Add(new KeyValInterpreter(kvp));
            
              
            compiler.FileFilter = "*aspx*";
            compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\RMA\2021";
            //compiler.Dest = compiler.Source + "\\_compiled";
            compiler.Compile();


            Cache.CacheEdit();



        }
    }
}
/*
 
 
             compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\sprocs\";
            compiler.FileFilter = "*frmval*.sql";
            compiler.Compile();

            compiler.ContentFormatter = (c) => (c.RemoveEmptyLines());
            compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\FismaForms\2021"; 
            compiler.FileFilter = "*.aspx*";
            compiler.Compile();

 
 
 */


