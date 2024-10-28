using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchJob.Data;
using SearchJob.Interfaces;
using SearchJob.Models;

namespace SearchJob.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    { private readonly JobDbContext _context;
        public FavoriteRepository(JobDbContext context) 
        {
            _context = context;
        }
        public async Task<Favorite> CreateAsync(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task<Favorite> DeleteJobFromFavorite(int id)
        {
            var favoriteModel = await _context.Favorites.FirstOrDefaultAsync(x => x.JobId == id);
            _context.Favorites.Remove(favoriteModel);
            await _context.SaveChangesAsync();
            return favoriteModel;
        }

        public async Task<List<Job>> GetUserFavoriteAsync(AppUser user)
        {   
            var favoriteModel = await _context.Favorites.Where(x => x.AppUserId == user.Id)
            .Select(job => new Job
            {
                Id = job.JobId,
                Title = job.Job.Title,
                Description = job.Job.Description,
                CompanyName = job.Job.CompanyName,
                Location = job.Job.Location,
                SalaryFrom = job.Job.SalaryFrom,
                SalaryTo = job.Job.SalaryTo,
                Url = job.Job.Url,
            }).ToListAsync();
            return favoriteModel;
        }

        
    }
}
