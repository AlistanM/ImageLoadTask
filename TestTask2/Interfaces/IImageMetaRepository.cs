using TestTask2.Repositories.DB;

namespace TestTask2.Interfaces
{
    public interface IImageMetaRepository
    {
        void Create(MetaInfo info);
        IEnumerable<MetaInfo> ReadAll();
        void Update(MetaInfo info);
        void Delete(long id);

    }
}
