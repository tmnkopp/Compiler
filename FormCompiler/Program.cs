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
using SOM.Parsers; 
using System.Reflection;
using Compiler.Models; 

namespace Compiler
{
    class Program
    {
        public static void Main(string[] args)
        {
            Cache.Write("");
            BODCompiler compiler = new BODCompiler();
            compiler.Run();
            Cache.CacheEdit();
        }
    }

    public class ReplaceCompiler  : BaseCompiler
    {
        public static string ProdPath = @"D:\\dev\\CyberScope\\CyberScopeBranch\\CSwebdev\\";
        public static string CodePath = $"{ProdPath}\\CustomControls\\HVAPoamAgency.ascx.vb";
        public ReplaceCompiler()
        {
            Source = CodePath;
            Dest = CodePath;
        }

    }
    
    public class ParseCompileReplace  : BaseCompiler
    {
        public static string ProdPath = @"D:\\dev\\CyberScope\\CyberScopeBranch\\CSwebdev\\";
        public static string CodePath = $"{ProdPath}\\CustomControls\\HVAPoamAgency.ascx.vb";
        public ParseCompileReplace()
        {
            Source = CodePath;
            Dest = CodePath;
        }

    }

    public class FileCompiler : BaseCompiler
    {
        public static string ProdPath = @"D:\\dev\\CyberScope\\CyberScopeBranch\\CSwebdev\\";
        public static string CodePath = $"{ProdPath}\\CustomControls\\HVAPoamAgency.ascx.vb"; 
        public FileCompiler()
        { 
            Source = CodePath;
            Dest = CodePath; 
        } 
    }
}
 




