using System.Xml.Serialization;
using System;

namespace TestTask2.Configuration
{
    public static class ConfigHelper
    {
        private const string Path = "./Config.xml";
        public static Configuration Configuration 
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = GetConfig();
                }
                return _configuration;
            }
        }
        private static Configuration _configuration;

        private static Configuration GetConfig() 
        {
            var xmlSerializer = new XmlSerializer(typeof(Configuration));

            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
               return xmlSerializer.Deserialize(fs) as Configuration;
            }
        }
    }
}
