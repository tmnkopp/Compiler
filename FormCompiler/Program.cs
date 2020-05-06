﻿using SOM.IO;
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
            CompilationBuilder<FileCompiler> compiler = new CompilationBuilder<FileCompiler>();
            compiler.Init()
                .CompileMode(CompileMode.Commit) 
                .ContentCompilers(
                    new List<ICompiler>() {
                    new JsonCompile("{  \"Q2\":\"Q3\"  }")
                })
                ;



            Cache.CacheEdit();
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
 




