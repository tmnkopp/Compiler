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
using SOM.Compilers; 
using System.Reflection;
using Compiler.Models;
using SOM.Formatters;
namespace Compiler
{ 
    class Program
    { 
        public static void Main(string[] args) 
        {
            Bootstrapper.Run();
            FileReader r = new FileReader($"{ AppSettings.SourceDir }_input.txt");
            string content = r.Read();
             
            ModelCompiler compiler;
             compiler = new ModelCompiler(
                "Clients",
                "Compiler.Formatters.NgInput, Compiler");



  

            content = compiler.Compile(content);
            Cache.Write(content);
            Cache.CacheEdit();

            Console.Read();

        }
    } 
}
 




