
using SOM.Formatters;
using SOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Formatters
{
    public class PropFormatter : ITypeFormatter<AppModelItem>
    {
        public string Format(AppModelItem type)        {
            string format = "public {1} {0} {{ get; set; }}\n";
            if (type.DataType.ToLower().Contains("int"))
            {
                return string.Format(format, type.Name, "integer");
            }
            return string.Format(format, type.Name, "string");
        }
    }
    public class NgInput : ITypeFormatter<AppModelItem>
    {
        public string Format(AppModelItem type)
        {
            string format = "<input type=\"text\" id=\"{0}\" formControlName =\"{0}\" class=\"form-control\" />\n";
            
            if (type.DataType.ToLower().Contains("int"))
            {
                return string.Format(format, type.Name, "int");
            }
            if (type.DataType.ToLower().Contains("date"))
            {
                return string.Format(format, type.Name, "DateTime");
            }

            return string.Format(format, type.Name, "string");
        }
    } 
}
