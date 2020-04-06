using SOM.IO;
using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Models
{
    public class TemplateFile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public List<IProcedure> ContentCompilers { get; set; }
    
    }
}
