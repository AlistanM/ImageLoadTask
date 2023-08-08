using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TestTask2.Repositories.DB;

namespace TestTask2.Models
{

    public class APIFileInfo
    {
        [JsonProperty("id")]
        public long? Id;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("description")]
        public string Description;

        public static APIFileInfo FromMeta(MetaInfo info) {

            return new APIFileInfo() { Id = info.Id, Name = info.Name, Description = info.Description };     
        }

        public static MetaInfo ToMeta(APIFileInfo info)
        {

            return new MetaInfo() { Id = info.Id, Name = info.Name, Description = info.Description };
        }
    }
}
