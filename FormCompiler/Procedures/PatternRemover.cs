using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compiler.Procedures
{
    public class PatternRemover 
    {
        private string _pattern =  "";
        private string _replacement = ""; 
        public PatternRemover(string Pattern)
        {
            _pattern = Pattern;
        }
        public PatternRemover(string Pattern , string Replacement)
        {
            _replacement = Replacement;
            _pattern = Pattern;
        }
        public string Execute(string content)
        {
            MatchCollection matches = Regex.Matches(content, _pattern);
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                {
                    string  target = capture.Value;
                    content = content.Replace(target,_replacement);
                }
            }
            return content;
        }
    }
}
