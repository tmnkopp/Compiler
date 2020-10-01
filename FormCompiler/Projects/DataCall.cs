using SOM.Procedures;
using SOM.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOM.IO;

namespace Compiler
{
    public static class DataCallCompiler  
    { 
        public static void Run()
        {
            Cache.Write("");
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("Q3", "Q4");  
            
            AppSettingsCompiler compiler = new AppSettingsCompiler(); 
            compiler.CompileMode = CompileMode.Debug;
            compiler.FilenameCompilers.Add(new RegexInterpreter(keyValuePairs));
            compiler.ContentCompilers.Add( new SqlKeyValInterpreter(compiler.Source + "\\keyval.sql"));
            compiler.ContentCompilers.Add(new RegexInterpreter(keyValuePairs));
            compiler.ContentCompilers.Add(new Incrementer(".*,(\\d{4}),.*", 1000));
            //compiler.ContentCompilers.Add(new Incrementer("\"(PK_key=\\d{4})\".*", 1000)); 
            //compiler.ContentCompilers.Add(new RegexInterpreter(keyValuePairs));  

            compiler.Source = AppSettings.SourceDir+"\\_compile";
            compiler.FileFilter = "*DBUpdate_Q4*.sql";
            compiler.Compile();


            compiler.FileFilter = "*.aspx*";
            compiler.Compile();
             

            Cache.CacheEdit();
 
        }
    }
}


