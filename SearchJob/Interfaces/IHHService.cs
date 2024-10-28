using SearchJob.Models;

namespace SearchJob.Interfaces
{
    public interface IHHService
    {
        Task<List<Job>> FindJobInHHAsync(int page);
    }
}
