using Microsoft.EntityFrameworkCore;
using SearchJob.Data;
using SearchJob.Interfaces;
using SearchJob.Models;

namespace SearchJob.Repository
{
    public class JobRepository : IJobRepository
    {   

        private readonly JobDbContext _context;

        public JobRepository(JobDbContext context)
        {
            _context = context;
        }
        public async Task<Job> CreateAsync(Job jobModel)
        {
            await _context.Jobs.AddAsync(jobModel);
            await _context.SaveChangesAsync();
            return jobModel;

        }

        public async Task<Job> DeleteAsync(int id)
        {
            var jobModel = await _context.Jobs.FindAsync(id);
            if (jobModel == null)
            {
                 return null;
            }
            _context.Jobs.Remove(jobModel);
            await _context.SaveChangesAsync();
            return jobModel;
        }

        public async Task<List<Job>> GetAsync()
        {
            var job = await _context.Jobs.ToListAsync();
            return job;
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            return job;
        }

    }
}
