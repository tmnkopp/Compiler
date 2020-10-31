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
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("2019", "2020");  
            
            AppSettingsCompiler compiler = new AppSettingsCompiler();
            compiler.CompileMode = CompileMode.Commit;
            compiler.Source = AppSettings.SourceDir + "\\_compile"; 
            compiler.FilenameCompilers.Add(new RegexInterpreter(keyValuePairs));
            compiler.ContentCompilers.Add(new MetricCommenter());
            compiler.ContentCompilers.Add( new SqlKeyValInterpreter(compiler.Source + "\\keyval.sql"));
            compiler.ContentCompilers.Add(new RegexInterpreter(keyValuePairs));
            
              
            compiler.FileFilter = "*dbupdate*.sql";
            compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\";
            compiler.Compile();

            compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\sprocs\";
            compiler.FileFilter = "*frmval*.sql";
            compiler.Compile();

            compiler.ContentFormatter = (c) => (c.RemoveEmptyLines());
            compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\AAPS\2020";
            compiler.FileFilter = "*.aspx*";
            compiler.Compile(); 

            //Cache.CacheEdit();


        }
    }
}


