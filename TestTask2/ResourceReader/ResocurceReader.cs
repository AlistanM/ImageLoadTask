using System.Reflection;
using System.Xml.Serialization;

namespace TestTask2.ResourceReader
{
    public static class ResocurceReader
    {
        public static T GetString<T>(string resourceKey) where T : class
        {
            Assembly assembly = typeof(ResocurceReader).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceKey))
            using (StreamReader reader = new StreamReader(stream))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return xmlSerializer.Deserialize(reader) as T;
            }
        }
    }
}
