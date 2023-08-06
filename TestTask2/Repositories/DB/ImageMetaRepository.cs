using System.Data.SqlClient;
using System.Data.SqlTypes;
using TestTask2.Configuration;
using TestTask2.Interfaces;
using TestTask2.Repositories.DB.ScriptsProvider;

namespace TestTask2.Repositories.DB
{
    public class ImageMetaRepository : IImageMetaRepository
    {
        private object[][] ExecuteScript(string script)
        {
            var result = new List<object[]>();
            var connectionString = ConfigHelper.Configuration.ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = script;

                using (var reader = sqlCommand.ExecuteReader())
                {
                    var row = new object[reader.FieldCount];
                    while (reader.Read())
                    {
                        reader.GetValues(row);
                        result.Add(row.ToArray());
                    }
                }
            }
            return result.ToArray();
        }
        private long GetMaxID()
        {
            var id = ExecuteScript(ScriptsProvider.ScriptsProvider.Scripts.GetMaxID);
            return (long)id.First().First();
        }
        public void Create(MetaInfo info)
        {
            var id = GetMaxID()+1;
            var str = string.Format(ScriptsProvider.ScriptsProvider.Scripts.Create, id, info.Name, info.Description);
            ExecuteScript(str);
        }
        public IEnumerable<MetaInfo> ReadAll()
        {
            var res = new List<MetaInfo>();
            var table =  ExecuteScript(ScriptsProvider.ScriptsProvider.Scripts.ReadAll);
            foreach (var row in table)
            {
                res.Add(new MetaInfo { Id = (long)row[0], Name = (string)row[1], Description = (string)row[2] });
            }
            return res;
        }

        public void Update(MetaInfo info)
        {
            var str = string.Format(ScriptsProvider.ScriptsProvider.Scripts.Update, info.Name, info.Description, info.Id);
            ExecuteScript(str);
        }
        public void Delete(long id)
        {
            var str = string.Format(ScriptsProvider.ScriptsProvider.Scripts.Delete, id);
            ExecuteScript(str);
        }
    }
}
