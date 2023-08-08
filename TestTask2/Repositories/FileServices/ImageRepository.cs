using TestTask2.Configuration;
using TestTask2.Interfaces;

namespace TestTask2.Repositories.FileServices
{
    public class ImageRepository : IImageRepository
    {
        public byte[] GetImage(long id)
        {
            var pat = ConfigHelper.Configuration.ImagePath;
            var path = Path.Combine(pat, $"{id}.dat");
            return File.ReadAllBytes(path);
        }

        public void SaveImage(long id, byte[] bytes)
        {
            var pat = ConfigHelper.Configuration.ImagePath;
            var path = Path.Combine(pat,$"{id}.dat");
            File.WriteAllBytes(path, bytes);
        }

        public void DeleteImage(long id)
        {
            var pat = ConfigHelper.Configuration.ImagePath;
            var path = Path.Combine(pat, $"{id}.dat");
            File.Delete(path);
        }
    }
}
