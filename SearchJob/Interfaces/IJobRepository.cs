using Microsoft.AspNetCore.Mvc;
using SearchJob.Models;

namespace SearchJob.Interfaces
{
    public interface IJobRepository
    {
        public Task<List<Job>> GetAsync();

        public Task<Job> GetByIdAsync(int id);

        public Task<Job> CreateAsync(Job jobModel);

        public Task<Job> DeleteAsync(int id);
    }
}
