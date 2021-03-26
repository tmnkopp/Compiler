
using CommandLine; 
using SOM;
using SOM.Compilers;
using SOM.IO;
using SOM.Parsers;
using SOM.Procedures;
using System;
using System.Collections.Generic;

namespace Compiler
{
    [Serializable]
    [Verb("compile", HelpText = "Command Runner.")]
    public class CompileOptions
    {
        [Option('t', "Task")]
        public string Task { get; set; }
        [Option('m', "CompileMode", Default = CompileMode.Cache)]
        public CompileMode CompileMode { get; set; }
        [Option('v', "Verbose", HelpText = "Print details during execution.")]
        public bool Verbose { get; set; }
    }

    public class Options
    {
        [Option('m', "CompileMode", Default=CompileMode.Cache)]
        public CompileMode ParseCompileModeMode { get; set; } 
        [Option('f', "FileFilter", Default = "*")]
        public string FileFilter { get; set; }
        [Option('t', "Type", Default = "")]
        public string Type { get; set; }
        [Option('v', "Verbose", HelpText = "Print details during execution.")]
        public bool Verbose { get; set; } 
    }
}