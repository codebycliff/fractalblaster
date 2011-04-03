using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FractalBlaster.Universe
{
    public static class Config
    {
        static Dictionary<string, string> Properties;


        static Config()
        {
            Properties = new Dictionary<string,string>();
            StreamReader configFile;
            try
            {
                configFile = new StreamReader("config.ini");
            
                string configLine;
                string propertyKey;
                string propertyValue;
                int i;
                while ((configLine = configFile.ReadLine()) != null)
                {
                    propertyKey = string.Empty;
                    propertyValue = string.Empty;
                    for (i = 0; (configLine[i] != '=') && (i < configLine.Length); i++)
                    {
                        propertyKey += configLine[i];
                    }
                    i++;
                    for (; i < configLine.Length; i++)
                    {
                        propertyValue += configLine[i];
                    }
                    Debug.printline(propertyKey + "=" + propertyValue);
                    Properties.Add(propertyKey, propertyValue);
                }
            }
            catch (Exception e)
            {
                Debug.printline("Config Exception! : " + e.GetType().ToString() + " , " + e.Message);
            }
        }

        public static string getProperty(string key)
        {
            string retValue;
            Properties.TryGetValue(key, out retValue);
            return retValue;
        }
    }
}
