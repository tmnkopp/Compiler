using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Compiler 
{
    public static class Assm
    {
        public static string NS = Assembly.GetExecutingAssembly().GetName().Name;
        public static string Path = Assembly.GetExecutingAssembly().Location.Replace("Compilers.dll", "");
        public static string AQF(this Type t)
        {
            var assm = Assembly.GetExecutingAssembly();
            return $"{t.FullName}, Compilers";
        }
    }
}
