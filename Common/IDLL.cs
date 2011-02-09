using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Common
{
    public interface IDLL
    {
        Type GetGenericType();
    }

    public static class DLLMaster
    {
        static List<IDLL> ValidDLL = new List<IDLL>();

        static DLLMaster()
        {
            List<Assembly> dlls = new List<Assembly>();
            List<Type> interfaces = new List<Type>();
            FileInfo[] files = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*.dll");

            // Get all DLLs
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    dlls.Add(Assembly.LoadFile(files[i].FullName));
                    interfaces.AddRange(dlls.Last().GetTypes());
                }
                catch (BadImageFormatException)
                {
                    Console.WriteLine("Skipping Loading UnManaged DLL " + files[i].FullName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("DLL Loading Exception: " + e.Message);
                }
            }

            // Filter on ones that implement this interface
            List<Type> dllList = interfaces.FindAll(delegate(Type t)
                {
                    List<Type> dllTypes = new List<Type>(t.GetInterfaces());
                    return dllTypes.Contains(typeof(IDLL));
                });

            ValidDLL = dllList.ConvertAll<IDLL>(delegate(Type t) { return Activator.CreateInstance(t) as IDLL; } );
        }

        public static Decoder getDecoder(string filepath)
        {
            Decoder rval = null;

            foreach (IDLL dll in ValidDLL)
            {
                if (dll.GetGenericType() == typeof(Decoder))
                {
                    rval = ((IDecode)dll).Generate(filepath);
                    break;
                }
            }

            // TODO: Perform check, if still null, we're missing a DLL, throw an exception

            return rval;
        }

        public static Output getOutput()
        {
            Output rval = null;

            foreach (IDLL dll in ValidDLL)
            {
                if (dll.GetGenericType() == typeof(Output))
                {
                    rval = ((IOutput)dll).Generate();
                    break;
                }
            }

            // TODO: Perform check, if still null, we're missing a DLL, throw an exception

            return rval;
        }
    }
}
