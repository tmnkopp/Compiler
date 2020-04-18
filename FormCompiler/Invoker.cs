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
    public class Invoker : BaseInvoker
    { 
        public Invoker( )
        {     
        }
        public object Execute(string InvocationCommand) {
            base.InvocationCommand = InvocationCommand;
            return base.Invoke();
        }  
    }
    public abstract class BaseInvoker
    {
        public string TypeNameFormat = "SOM.Procedures.{0}, SOM";
        public string InvocationCommand = "";
        public BaseInvoker( )
        { 
        }
        public BaseInvoker(string InvocationCommand)
        {
            this.InvocationCommand = InvocationCommand.RemoveAsChars("[]");
        } 
        public object Invoke()
        {  
            string[] commands = InvocationCommand.Split(new string[] { " -" }, StringSplitOptions.None);
            string TypeName = string.Format(this.TypeNameFormat, commands[0] );
            Type type = Type.GetType(TypeName);
            ConstructorInfo ctor = type.GetConstructors()[0];
            ParameterInfo[] parms = GetConstructorParams(commands, ctor);
            return ctor.Invoke(parms); 
        }
        private ParameterInfo[] GetConstructorParams(string[] parms, ConstructorInfo ctor)
        {
            ParameterInfo[] PI = ctor.GetParameters();
            object[] typeParams = new object[PI.Count()];
            int i = 0;
            foreach (ParameterInfo parm in PI)
            {
                if (parm.ParameterType == typeof(int))
                {
                    typeParams[i] = Convert.ToInt32(parms[i + 1]);
                }
                else
                {
                    typeParams[i] = parms[i + 1].RemoveAsChars("'");
                }
                i++;
            }
            return PI;
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
