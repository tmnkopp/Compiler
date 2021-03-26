using SOM.Procedures;
using SOM.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOM.IO;
using SOM.Parsers; 
using SOM.Extentions;
using SOM.Data;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Compiler
{
    public interface ICompile {
        void Execute();
    }
    public class EinsteinCompiler : ICompile
    {
        private readonly IAppSettings appSettings; 
        private readonly ISomCompiler compiler; 
        private readonly ILogger logger; 
        public EinsteinCompiler(
            IAppSettings appSettings,
            ISomCompiler somCompiler,
            ILogger logger)
        {
            this.compiler = somCompiler;
            this.appSettings = appSettings;
            this.logger = logger;
            this.compiler.Source = appSettings.SourceDir;
            this.compiler.Dest = appSettings.DestDir; 
        } 
        public void Execute()
        { 
            Func<string, string> replace = (n) => (n.Replace("EinsteinTaps", "EinsteinNetworkChannels"));
            compiler.CompileMode = CompileMode.Debug;
            compiler.FileFilter = "*.ascx*";
            compiler.FileNameFormatter = replace;
            compiler.ContentFormatter = replace; 
            compiler.Compile();
        }
    }
}
 
