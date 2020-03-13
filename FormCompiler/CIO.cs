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
    class CIO
    { 
        public static void Main(string[] args)
        {
            Cache.Write(""); 
            Console.ForegroundColor = ConsoleColor.Green; 
            GenerateNewForms(); 
            Cache.CacheEdit();

        }
        private static void GenDBScript()
        { 
            string root = @"C:\temp\templates\CIO2020Q2\DB_Update7.19_CIOQ120.sql"; 
            string dest = @"C:\temp\templates\CIO2020Q2\compiled\DB_Update7.22_CIOQ220.sql";

            string content = new FileReader(root).Read().ToString();
            content = new SqlKeyValCompile(@"C:\temp\templates\KVScriptForm.sql").Execute(content);

            FileWriter fw = new FileWriter(dest);
            fw.Write(content);
            Cache.Append(content);
        }


        private static void GenerateNewForms()
        {
            string root = @"C:\temp\templates\CIO2020Q2";
            string dest = @"D:\dev\CyberScope\CyberScopeBranch\CSwebdev\code\CyberScope\FismaForms\2020";
            //dest =   @"C:\temp\templates\CIO2020Q2\compiled"; 
            string content = "";
            DirectoryInfo DI = new DirectoryInfo($"{root}");  
            foreach (var file in DI.GetFiles("2020_Q1_CIO_*", SearchOption.TopDirectoryOnly))
            {
                content = new FileReader(file.FullName).Read().ToString();
                content = new SqlKeyValCompile(@"C:\temp\templates\KVScriptForm.sql").Execute(content);
          
                string newfile = $"{dest}\\{GetFileName(file.Name)}";
                FileWriter fw = new FileWriter(newfile);
                fw.Write(content);
                 
                content = new FileReader(newfile).Read().ToString();

                Dictionary<string, string> FoundKeys = new Dictionary<string, string>();
                MatchCollection matches = Regex.Matches(content, "PK_Question=\"\\d{5}");
                foreach (Match match in matches)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        string PK = capture.Value.Replace("\"", "").Replace("PK_Question=", "");
                        string target = new BlockExtractor("qpk", capture.Value, "<tr", "/tr>").Execute(content);
                        if (target != "" && !FoundKeys.ContainsKey(PK)) {
                            FoundKeys.Add(PK, capture.Index.ToString()); 
                            content = content.Replace(target, string.Format("{0}{2}{1}\n", QuestionInfo(PK), target, Utils.prefix));
                        }
                    }
                }
             
                matches = Regex.Matches(content, "Group\" PK_key=\"\\d{4,5}");
                foreach (Match match in matches)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        string PK = capture.Value.Replace("\"", "").Replace("Group PK_key=", "");
                        string target = new BlockExtractor("qgpk", capture.Value, "<tr", "/tr>").Execute(content);
                        if (target != "" && !FoundKeys.ContainsKey(PK))
                        {
                            FoundKeys.Add(PK, capture.Index.ToString());
                            content = content.Replace(target, string.Format("{0}{2}{1}\n", GroupInfo(PK), target, Utils.prefix));
                        } 
                    }
                }
                matches = Regex.Matches(content, " PK_key=\"\\d{5}");
                foreach (Match match in matches)
                { 
                    foreach (Capture capture in match.Captures)
                    {
                        string PK = capture.Value.Replace("\"", "").Replace(" PK_key=", "");
                        string target = new BlockExtractor("pk", capture.Value, "<tr", "/tr>").Execute(content);
                        if (target != "" && !FoundKeys.ContainsKey(PK))
                        {
                            FoundKeys.Add(PK, capture.Index.ToString());
                            content = content.Replace(target, string.Format("{0}{2}{1}\n", QuestionInfo(PK), target, Utils.prefix));
                        }
                    }
                }
                if (newfile.EndsWith(".aspx")) {
                    content = content.RemoveEmptyLines();
                }
               
                fw = new FileWriter(newfile);
                fw.Write(content);

            } 
        }
        private static string GetFileName(string fname)
        {
            fname = fname.Replace("Q1", "Q2");
            return fname;
        }

        private static void Process_FRMVAL()
        {
            string root = @"C:\temp\templates\CIO2020Q2\frmVal_2020Q1CIO.sql";
            string dest  = @"C:\temp\templates\CIO2020Q2\compiled\frmVal_2020Q2CIO.sql";

            string content = new FileReader(root).Read().ToString();
            content = new SqlKeyValCompile(@"C:\temp\templates\KVFrmVal.sql").Execute(content);

            FileWriter fw = new FileWriter(dest);
            fw.Write(content);
            Cache.Append(content); 
        }


        private static string GroupInfo(string PK_QuestionGroup)
        {
            StringBuilder SB = new StringBuilder();
            if (PK_QuestionGroup == "<!--null-->")
                return "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CAClientConnectionString"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM fsma_QuestionGroups WHERE PK_QuestionGroup={PK_QuestionGroup}", conn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        { 
                            SB.Append(Utils.prefix + "{");
                            SB.AppendFormat("{1}\"PK_QuestionGroup\":\"{0}\",", rdr["PK_QuestionGroup"].ToString(), Utils.prefix);
                            SB.AppendFormat("{1}\"GroupName\":\"{0}\",", rdr["GroupName"].ToString(), Utils.prefix);
                            SB.AppendFormat("{1}\"PK_Form\":\"{0}\",", rdr["PK_Form"].ToString(), Utils.prefix);
                            SB.AppendFormat("{1}\"FK_FormPage\":\"{0}\",", rdr["FK_FormPage"].ToString().Replace("\"", "'"), Utils.prefix);
                            SB.AppendFormat("{1}\"Text\":\"{0}\"", rdr["Text"].ToString().Replace("\"", "'"), Utils.prefix);
                            SB.Append(Utils.prefix + "}\n");
                        }
                    }
                }
            }
            return string.Format("<!--fsma_QuestionGroup{1}{0}{1}-->", SB.ToString().Trim(), Utils.prefix);
        }

        private static string QuestionInfo(string PK_Question) {
            StringBuilder SB = new StringBuilder();
            if (PK_Question == "<!--null-->")
                return "";
            Console.Write($"{PK_Question} , ");
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CAClientConnectionString"].ConnectionString))
            {
                conn.Open(); 
                using (SqlCommand cmd = new SqlCommand($"SELECT TOP 1 * FROM fsma_Questions WHERE PK_Question={PK_Question}", conn))
                { 
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        { 
                            SB.Append(Utils.prefix + "{");
                            SB.AppendFormat("{1}\"PK_Question\":\"{0}\",", rdr["PK_Question"].ToString(), Utils.prefix);
                            SB.AppendFormat("{1}\"identifier_text\":\"{0}\",", rdr["identifier_text"].ToString(), Utils.prefix) ;
                            SB.AppendFormat("{1}\"FK_QuestionGroup\":\"{0}\",", rdr["FK_QuestionGroup"].ToString(), Utils.prefix);
                            SB.AppendFormat("{1}\"QuestionText\":\"{0}\",", rdr["QuestionText"].ToString().Replace("\"","'"), Utils.prefix);
                            SB.AppendFormat("{1}\"FK_QuestionType\":\"{0}\"", rdr["FK_QuestionType"].ToString(), Utils.prefix);
                            SB.Append(Utils.prefix+"}\n" );
                        }
                    }
                }
            }
            return string.Format("<!--fsma_Question{1}{0}{1}-->", SB.ToString().Trim(), Utils.prefix); 
        }
        private static void GenerateNewControlTemplate()
        {
            StringBuilder sb = new StringBuilder();
            string content, template;
            template = new FileReader(@"C:\temp\templates\CBNumeric.txt").Read().ToString();
            for (int i = 30; i < 38; i++)
            {
                sb.Append(template.Replace("[CBNID1]", i.ToString()));
            }
            content = sb.ToString();
            content = new IndexCompile(50, 0, "[CB_ID_1]").Execute(content);
            content = new IndexCompile(5, 0, "[PKQ]").Execute(content);

            //Cache.Append(content);
            //Cache.CacheEdit();

            FileWriter fw = new FileWriter($"C:\\temp\\templates\\CIO2020Q11-3-5.txt");
            fw.Write(content);

        } 
    }

}



//StringBuilder result = new StringBuilder();
//string[] lines = content.Split('\n');
//foreach (var line in lines)
//{
//    if (!line.Contains("--[rmline]"))
//        result.AppendFormat("{0}\n", line); 
//}
//content = result.ToString(); 





