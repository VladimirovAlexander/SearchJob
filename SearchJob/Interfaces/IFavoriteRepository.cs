using SearchJob.Models;

namespace SearchJob.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Job>> GetUserFavoriteAsync(AppUser user);
        Task<Favorite> CreateAsync(Favorite favorite);

        Task<Favorite> DeleteJobFromFavorite(int id);

        
    }
}
