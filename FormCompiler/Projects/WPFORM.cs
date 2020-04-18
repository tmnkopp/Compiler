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
using SOM.Procedures;
using System.Reflection;
using Compiler.Models; 

namespace Compiler.Projects
    {
    class WPFORM
    {
        public static void Main(string[] args)
        {
            //Bootstrapper.Run(); 
            Dictionary<string, string> allFields = new Dictionary<string, string>();
            StringBuilder result = new StringBuilder();
            Cache.Write("");
            FileReader fr = new FileReader($"{AppSettings.SourceDir}\\_in.txt");
            string[] lines = fr.Read().Split('\n');
            foreach (var line in lines)
            {
                string label = line.Replace("\r", "");
                string item = label.FormatItem();
                result.Append($"<label>{label}</label>[text {item}]\n");
                allFields.Add(item, label);
            }
            Cache.Append(result.ToString());

            fr = new FileReader($"{AppSettings.SourceDir}\\_textarea.txt");
            lines = fr.Read().Split('\n');
            foreach (var line in lines)
            {
                string label = line.Replace("\r", "");
                string item = label.FormatItem();
                result.AppendFormat(WPFORM.col1, label, $"[textarea {item}]");
                allFields.Add(item, label);
            }
            Cache.Append(result.ToString());
            fr = new FileReader($"{AppSettings.SourceDir}\\_metrics.txt");
            lines = fr.Read().Split('\n');
            foreach (var line in lines)
            {
                string label = line.Replace("\r", "");
                string item = label.FormatItem();
                result.AppendFormat(WPFORM.col2, "", label, $"[select {item} \"Excellent\" \"Good\" \"Fair\" \"Poor\"]");
                allFields.Add(item, label);
            }
            Cache.Append(result.ToString());


            foreach (KeyValuePair<string, string> kvp in allFields)
            {
                result.Append($"{kvp.Value}: [{kvp.Key}]\n");

            }
            Cache.Write(result.ToString()); 
            Cache.CacheEdit();
  
        }

        public static string col2
        {
            get
            {
                FileReader fr = new FileReader($"{AppSettings.SourceDir}\\_2colwrap.txt");
                return fr.Read();
            }
        }
        public static string col1
        {
            get
            {
                FileReader fr = new FileReader($"{AppSettings.SourceDir}\\_1colwrap.txt");
                return fr.Read();
            }
        }
    }
}






