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

            //Func<PropDefinition, string> converter = (propdef) => (string.Format("this.{0} == form.{0};\n", propdef.NAME));
            Func<PropDefinition, string> format = (propdef) =>
            (
                 propdef.DATA_TYPE.Contains("int")  ? "1" : "2"  
                //string.Format("this.{0} == form.{0};\n", propdef.NAME)
            );
             
            TypeModelCompiler p = new TypeModelCompiler(
                            "Compiler.Models.Invoice, Compiler",
                            new Propformatter(format),
                            (c, r) => c.Replace("[model]", r) 
                        );
            string result = p.Execute(fr.Read());
            FileWriter w = new FileWriter($"{AppSettings.SourceDir}\\class.txt");
            w.Write($"{result}", true);
                   
        }
    } 
}
 




