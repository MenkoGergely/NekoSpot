
namespace NekoSpot
{
    public interface ICatDatabase
    {
        Task CreateCatAsync(Cat cat);
        Task DeleteCatAsync(Cat cat);
        Task<Cat> GetCatAsync(int id);
        Task<List<Cat>> GetCatsAsync();
        Task UpdateCatAsync(Cat cat);
    }
}