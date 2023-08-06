namespace TestTask2.Interfaces
{
    public interface IImageRepository
    {
        byte[] GetImage(long id);
        void SaveImage(long id, byte[] bytes);
    }
}
