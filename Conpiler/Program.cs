﻿using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using CommandLine;
using CommandLine.Text;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Newtonsoft.Json;
using SOM.Compilers;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = RegisterServices(args);
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
            IAppSettings appSettings = serviceProvider.GetService<IAppSettings>();
            ICompiler compiler = serviceProvider.GetService<ICompiler>();
            ILogger logger = serviceProvider.GetService<ILogger<Program>>();

            var exit = Parser.Default.ParseArguments<CompileOptions, Options>(args)
                .MapResult(
                (CompileOptions o) => {

                    logger.LogInformation("{o}", JsonConvert.SerializeObject(o));
                    CompileMode m = o.CompileMode;
                    //compiler
                    return 0;

                },
                (Options o) => {

                    logger.LogInformation("{o}", JsonConvert.SerializeObject(o));
                     
                    return 0;

                }, 
                errs => 1);
        }
        private static ServiceProvider RegisterServices(string[] args)
        { 
            var p = @"C:\Users\Tim\source\repos\SledgeOMatic\SledgeOMatic\bin\Debug\netcoreapp3.1\";

            IConfiguration configuration = new ConfigurationBuilder()
                  .SetBasePath(p)
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .AddCommandLine(args)
                  .Build();

            var services = new ServiceCollection();
            services.AddLogging(cfg => cfg.AddConsole());
            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<Program>>());
            services.AddSingleton(configuration);
            services.AddTransient<IAppSettings, AppSettings>(); 
            services.AddTransient<ICompiler, SOM.Compilers.Compiler>(); 
            return services.BuildServiceProvider();
        }
    } 
}
