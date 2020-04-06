using SOM.Extentions;
using SOM.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Compiler
{
    public static class Invoker  
    {
        public static object InvokeProcedure(string InvocationCommand)
        {
            string[] commands = InvocationCommand.Split(new string[] { " -" }, StringSplitOptions.None);

            Type type = Type.GetType($"SOM.Procedures.{commands[0]}, SOM");
            ConstructorInfo ctor = type.GetConstructors()[0];
            ParameterInfo[] PI = ctor.GetParameters();
            object[] typeParams = new object[PI.Count()];
            int i = 0;
            foreach (ParameterInfo parm in PI)
            {
                if (parm.ParameterType == typeof(int))
                {
                    typeParams[i] = Convert.ToInt32(commands[i + 1]);
                }
                else
                {
                    typeParams[i] = commands[i + 1].RemoveAsChars("'");
                }
                i++;
            }
            object procedure = ctor.Invoke(typeParams);
            return procedure;
        }
    }
    public static class TypeUtils
    {
        public static bool IsInt(object arg)
        {
            try
            {
                int i = Convert.ToInt32(arg);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
