using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler 
{
    public static class AppSettings
    {
        
        public static string BasePath = ConfigurationManager.AppSettings["BasePath"].ToString();
        public static string SourceDir = ConfigurationManager.AppSettings["SourceDir"].ToString();
        public static string DestDir = ConfigurationManager.AppSettings["DestDir"].ToString();
        public static string AssmFormat = "Compiler.{0}.{1}, Compiler";
        public static string AssmFormatters = "Compiler.Formatters.{0}, Compiler";
        public static string AssmCompilers = "Compiler.Compilers.{0}, Compiler";
    }
}
