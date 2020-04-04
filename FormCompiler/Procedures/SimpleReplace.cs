using Compiler;
using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Compiler
{
    
    public class JsonReplace : IProcedure
    {
        private Dictionary<string, string> dict;

        public JsonReplace(string json)
        {
            dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
        public string Execute(string content)
        {
            foreach (var kv in dict)
            {
                content = content.Replace(kv.Key, kv.Value);
            } 
            return content;
        }
    }
}
 