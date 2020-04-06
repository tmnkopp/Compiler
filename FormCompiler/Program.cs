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
using SOM.Procedures.Data;

namespace Compiler
{ 
    class Program
    { 
        public static void Main(string[] args) 
        {
            Bootstrapper.Run(); 
            
            FileReader fr = new FileReader($"{AppSettings.SourceDir}\\_class.txt");   
            Type type = Type.GetType("Compiler.Models.TemplateFile, Compiler");
             
            Func<PropDefinition, string> converter = (propdef) => (string.Format("this.{0} == form.{0};\n", propdef.NAME));
            Func<string, string, string> integrator = (c, r) => c.Replace("[fields]", r);
            //NgFormFromModel
            //NgInterfaceFromModel
            IProcedure p = new TypeModelCompiler(
                            "Compiler.Models.Invoice, Compiler",
                            new ccNgInput(),
                            (c, r) => c.Replace("[fields]", r).Replace("/>", "/>\n")
                        );
            string result = p.Execute(fr.Read());
            FileWriter w = new FileWriter($"{AppSettings.SourceDir}\\class.txt");
            w.Write($"{result}", true);
            CompilationBuilder<AppSettingsCompiler> compiler = new CompilationBuilder<AppSettingsCompiler>();

            // CompilationBuilder<AppSettingsCompiler> compiler = new CompilationBuilder<AppSettingsCompiler>();
            // compiler.Init()
            //     .CompileMode(CompileMode.Commit) 
            //     .FileFilter("*_class.txt")
            //     .ContentCompilation(
            //         new List<IProcedure> {
            //             new TableModelCompiler(
            //                 "fsma_Questions",
            //                 new ccCustomConverter(converter),
            //                 (c,r) => c.Replace("[fields]", r)
            //             ),
            //             new JsonCompile("{\"[entity]\":\"Question\"}")
            //         }
            //     )
            //     .FilenameCompilation(
            //             new List<IProcedure> { new JsonCompile("{\"_in\":\"_question\"}") }
            //     )
            //     .Compile() ;

            //
            //    TemplateWriter TB = new TemplateWriter(
            //        new List<TemplateFile>(){
            //            new TemplateFile(){ Path=@"c:\temp\$testing\1\test.html", Content="[ind]", ContentCompilers=procs }
            //        } 
            //    );
            //    TB.CreateDirectories=true;
            //    TB.Build();
            //

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
 




