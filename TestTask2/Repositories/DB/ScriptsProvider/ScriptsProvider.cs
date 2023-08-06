using System.Xml.Serialization;
using TestTask2.ResourceReader;

namespace TestTask2.Repositories.DB.ScriptsProvider
{
    public class ScriptsProvider
    {
        private const string ResourceKey = "TestTask2.ResourceReader.Resources.SQL.xml";
        public static Scripts Scripts
        {
            get
            {
                if (_scripts == null)
                {
                    _scripts = GetScripts();
                }
                return _scripts;
            }
        }
        private static Scripts _scripts;

        private static Scripts GetScripts()
        {
            return ResocurceReader.GetString<Scripts>(ResourceKey);      
        }
    }
}
