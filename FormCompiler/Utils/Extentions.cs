using SOM.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public static class Extentions
    { 
        public static string FormatItem(this string item)
        {
            return item.Replace(" ", "_").RemoveAsChars(",.-=+: ?/{}() '");
        }
    }
}
