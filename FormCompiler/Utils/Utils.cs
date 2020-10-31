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

namespace Compiler
{

    public static class Utils {  
   

        public static string prefix = "\n\t\t\t";
        public static Dictionary<string, string> FoundKeys = new Dictionary<string, string>();
   
         
        public static string GroupInfo(string PK_QuestionGroup)
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

        public  static string QuestionInfo(string PK_Question)
        {
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
                            SB.AppendFormat("{1}\"identifier_text\":\"{0}\",", rdr["identifier_text"].ToString(), Utils.prefix);
                            SB.AppendFormat("{1}\"FK_QuestionGroup\":\"{0}\",", rdr["FK_QuestionGroup"].ToString(), Utils.prefix);
                            SB.AppendFormat("{1}\"QuestionText\":\"{0}\",", rdr["QuestionText"].ToString().Replace("\"", "'"), Utils.prefix);
                            SB.AppendFormat("{1}\"FK_QuestionType\":\"{0}\"", rdr["FK_QuestionType"].ToString(), Utils.prefix);
                            SB.Append(Utils.prefix + "}\n");
                        }
                    }
                }
            }
            return string.Format("<!--fsma_Question{1}{0}{1}-->", SB.ToString().Trim(), Utils.prefix);
        }
    }
}
