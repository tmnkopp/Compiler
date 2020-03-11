using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormCompiler
{
    public interface IRegexInject
    {
        string Execute(string content);
    }
    public class PK_QuestionInject : IRegexInject { 
        public string Execute(string content)
        {
            MatchCollection matches = Regex.Matches(content, "PK_Question=\"\\d{5}");
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    string PK = capture.Value.Replace("\"", "").Replace("PK_Question=", "");
                    string target = new BlockExtractor("qpk", capture.Value, "<tr", "/tr>").Execute(content);
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
