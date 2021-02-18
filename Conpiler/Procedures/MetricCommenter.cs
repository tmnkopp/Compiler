using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compiler.Procedures
{
    class MetricCommenter : IInterpreter
    {
        private  Dictionary<string, string> FoundKeys = new Dictionary<string, string>();
        public string Interpret(string content)
        {
            MatchCollection matches = Regex.Matches(content, "['|\"](\\d{5})");
            foreach (Match match in matches)
            { 
                string targetmatch =  match.Groups[0].Value;
                string pk = match.Groups[1].Value;
                if (!FoundKeys.ContainsKey(pk)) {
                    FoundKeys.Add(pk, targetmatch);
                    RangeExtractor parser = new RangeExtractor(targetmatch, "<tr", "/tr>");
                    foreach (var item in parser.Parse(content)) 
                        content = content.Replace(item, string.Format("{0}{2}{1}\n", Utils.QuestionInfo(pk), item, Utils.prefix));
                  
                } 
            }
            return content; 
        }
    } 
}
