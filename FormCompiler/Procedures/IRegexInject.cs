using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compiler
{ 
    public class FrmValInject : SOM.Procedures.IInterpreter
    { 
        public string Interpret(string content)
        {
            MatchCollection matches = Regex.Matches(content, "PK_Question = \\d{5}");
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    string PK = capture.Value.Replace("\"", "").Replace("PK_Question = ", "");
                    string target = new BlockExtractor( capture.Value, "IF", "END").Parse(content);
                    if (target != "" )
                    {
                        content = content.Replace(target, string.Format("{0}{2}{1}\n", Utils.QuestionInfo(PK), target, Utils.prefix));
                    }
                }
            }
            return content;
        }
    }    
    public class PK_QuestionInject : SOM.Procedures.IInterpreter
    { 
        public string Interpret(string content)
        {
            MatchCollection matches = Regex.Matches(content, "PK_Question=\"\\d{5}");
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    string PK = capture.Value.Replace("\"", "").Replace("PK_Question=", "");
                    string target = new BlockExtractor( capture.Value, "<tr", "/tr>").Parse(content);
                    if (target != "" )
                    {
                        content = content.Replace(target, string.Format("{0}{2}{1}\n", Utils.QuestionInfo(PK), target, Utils.prefix));
                    }
                }
            }
            return content;
        }
    }
}
