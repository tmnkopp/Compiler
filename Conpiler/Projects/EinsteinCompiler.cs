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
    public static class EinsteinCompiler  
    { 
        public static void Run()
        {
            //EinsteinBGP_CRUD
            Cache.Write("");
            Dictionary<string, string> kvp = new Dictionary<string, string>();
            kvp.Add("EinsteinBGP", "EinsteinUnmonitoredTraffic"); 
            
            AppSettingsCompiler compiler = new AppSettingsCompiler();
            compiler.CompileMode = CompileMode.Cache;
            compiler.Source = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\database\Sprocs\";
            compiler.FileFilter = "*EinsteinBGP*.sql";
            compiler.FilenameCompilers.Add(new KeyValInterpreter(kvp));
            //compiler.ContentCompilers.Add(new MetricCommenter());
            compiler.ContentCompilers.Add(new SqlKeyValInterpreter(@"C:\_som\_src\_compile\keyval.sql"));
            compiler.ContentCompilers.Add(new KeyValInterpreter(kvp));

            //compiler.FileFilter = "*aspx*";
            //compiler.Dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\RMA\2021";
            //compiler.Dest = compiler.Source + "\\_compiled";
            compiler.Compile(); 
            Cache.CacheEdit(); 
        }
    }
}
 
