using TestTask2.Interfaces;

namespace TestTask2.Repositories.FileServices
{
    public class ImageRepository : IImageRepository
    {
        public byte[] GetImage(long id)
        {
            return File.ReadAllBytes($"{id}.dat");
        }

        public void SaveImage(long id, byte[] bytes)
        {
            File.WriteAllBytes($"{id}.dat", bytes);
            
        }
    }
}
