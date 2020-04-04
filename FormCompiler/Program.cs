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
using Newtonsoft.Json;

namespace Compiler
{ 
    class Program
    { 
        public static void Main(string[] args) 
        {
     
            CompilationBuilder<DirectoryRecompiler> compiler = new CompilationBuilder<DirectoryRecompiler>();
            FileWriter w = new FileWriter("c:\\temp\\testing\\TEMP.txt");
            w.Write("[INDEX]", true);
            w = new FileWriter("c:\\temp\\testing\\TEMP1.txt");
            w.Write("[INDEX1]", true);

            compiler.Init()
                .CompileMode(CompileMode.Commit)
                .Source("c:\\temp\\testing\\") 
                .Dest("c:\\temp\\$testing\\", true) 
                .ContentCompilation(
                    new List<IProcedure> { 
                    new JsonReplace("{\"1\":\"2\"}")
                    }
                )
                .FilenameCompilation(
                    new List<IProcedure> {
                    new JsonReplace("{\".txt\":\".doc\"}"),
                    new JsonReplace("{\"1\":\"2\"}")
                    }
                )
                .Compile(); 

        } 
    } 
}
 




