using SearchJob.Models;

namespace SearchJob.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Job>> GetUserFavorite(AppUser user);
        Task<Favorite> CreateAsync(Favorite favorite);

        Task<Favorite> DeleteJobFromFavorite(int id);

        
    }
}
