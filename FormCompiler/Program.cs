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
using System.Reflection;

namespace Compiler
{ 
    class Program
    { 
        public static void Main(string[] args) 
        {

            IProcedure obj = new RepeaterCompile(1, 10);
            Tuple<string, string, List<IProcedure>> tup 
            = new Tuple<string, string, List<IProcedure>>("","", new List<IProcedure> { new RepeaterCompile(1, 10) });

            DirectoryBuilder fsc = new DirectoryBuilder( 
                new Dictionary<string, string>() { 
                { "c:\\temp\\testing\\temp-1.txt","[index]"}
                ,{ "c:\\temp\\testing\\temp-2.txt","[index]"}
            }, 
                new List<IProcedure>() { 
                    new RepeaterCompile(1,15)
            });
            fsc.Build();
            Type type = Type.GetType("SOM.Procedures.RepeaterCompile, SOM");
            ConstructorInfo ctor = type.GetConstructors()[0];
            
            IProcedure proc = (IProcedure)Invoker.InvokeProcedure("RepeaterCompile -1 -15");

            
            FileWriter w = new FileWriter("c:\\temp\\testing\\temp.txt");
            w.Write("[INDEX]", true);

            FileReader r = new FileReader("c:\\temp\\testing\\temp.txt");
            string content = r.Read();

            content = proc.Execute(content);
            Cache.Write(content);
            Cache.CacheEdit();
            
            
            
            //CompilationBuilder<DirectoryRecompiler> compiler = new CompilationBuilder<DirectoryRecompiler>();
            //IProcedure proc = new RepeaterCompile(

         //   compiler.Init()
         //       .CompileMode(CompileMode.Commit)
         //       .Source("c:\\temp\\testing\\") 
         //       .Dest("c:\\temp\\$testing\\", true) 
         //       .ContentCompilation(
         //           new List<IProcedure> { 
         //           new JsonReplace("{\"1\":\"2\"}")
         //           }
         //       )
         //       .FilenameCompilation(
         //           new List<IProcedure> {
         //           new JsonReplace("{\".txt\":\".doc\"}"),
         //           new JsonReplace("{\"1\":\"2\"}")
         //           }
         //       )
         //       .Compile(); 
         //
        } 
    } 
}
 




