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
namespace FormCompiler
{
// Process_FRMVAL();
 // GenDBScript();
    class RMA
    {
      
        public static void Main(string[] args)
        {
            Cache.Write(""); 
            Console.ForegroundColor = ConsoleColor.Green; 
            GenerateNewForms(); 
            Cache.CacheEdit();

        }
        private static string GetFileName(string fname)
        {
            fname = fname.Replace("Q1", "Q2");
            return fname;
        }
        private static void GenerateNewForms() 
        {
            string root = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\RMA\2020";
            string dest = @"C:\temp\templates\CIO\compiled";
           
            string content = "";
            DirectoryInfo DI = new DirectoryInfo($"{root}");  
            foreach (var file in DI.GetFiles("2020_Q1_RMA_*", SearchOption.TopDirectoryOnly))
            {
                content = new FileReader(file.FullName).Read().ToString();
                content = new SqlKeyValCompile(@"C:\temp\templates\CIO\KVScriptForm.sql").Execute(content);
                string renamed = new SqlKeyValCompile(@"C:\temp\templates\CIO\KVScriptForm.sql").Execute(file.Name);
                string newfile = $"{dest}\\{renamed}";
                FileWriter fw = new FileWriter(newfile);
                fw.Write(content);
                 
                content = new FileReader(newfile).Read().ToString();
                content = Utils.PK_QuestionInject(content);
                content = Utils.PKGroupInject(content);
                content = Utils.PK_KeyInject(content);
                 
                if (newfile.EndsWith(".aspx")) {
                    content = content.RemoveEmptyLines();
                }
               
                fw = new FileWriter(newfile);
                fw.Write(content); 
            } 
        }

       
    } 
}

 

