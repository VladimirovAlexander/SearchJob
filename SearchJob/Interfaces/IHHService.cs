using SearchJob.Models;

namespace SearchJob.Interfaces
{
    public interface IHHService
    {
        Task<Job> FindJobByTitleAsync(string title);
    }
}
