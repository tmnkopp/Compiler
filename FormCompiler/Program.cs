using SOM.IO;
using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SOM.Extentions;
using SOM; 
using System.Reflection;
using Compiler.Models;
using SOM.Compilers;

namespace Compiler
{
    class Program
    {
        public static void Main(string[] args)
        {
            Cache.Write("");
            AppSettingsCompiler compiler = new AppSettingsCompiler();
            compiler.Source  = AppSettings.SourceDir; 
            compiler.FileFilter = "*.cshtml";
            compiler.ContentCompilers.Add(new ModelTemplateInterpreter()); 
            compiler.ContentCompilers.Add(new ModuloInterpreter()); 
            compiler.CompileMode = CompileMode.Cache;
            compiler.Compile(); 
            Cache.CacheEdit();

            //DataCallCompiler.Run();
        }
    } 
    class ReplaceByRegex : BaseRegexInterpreter
    {
        public ReplaceByRegex()
        { 
            this.KeyVals.Add("nvarchar", "string"); 
        }
        public override string Interpret(string content)
        {
            return base.Interpret(content);
        }
    }
}
 




