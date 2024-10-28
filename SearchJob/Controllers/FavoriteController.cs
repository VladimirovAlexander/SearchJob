using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using SearchJob.Dtos.Job;
using SearchJob.Interfaces;
using SearchJob.Mappers;
using SearchJob.Models;
using System.Security.Claims;

namespace SearchJob.Controllers
{
    
    [Route("api/favorites")]
    public class FavoriteController:Controller 
    {
        private readonly UserManager<AppUser> _userManager;
        
        private readonly IFavoriteRepository _favoriteRepo;

        private readonly IJobRepository _jobRepo;

        public FavoriteController(UserManager<AppUser> userManager, IFavoriteRepository favoriteRepo, IJobRepository jobRepo) 
        {

            _userManager = userManager;
            _favoriteRepo = favoriteRepo;
            _jobRepo = jobRepo;            

        }

        [Authorize]
        [HttpPost("AddJobToFavorites")]
        public async Task<IActionResult> AddFavorite([FromForm] Job job)
        {   
           
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var appUser = await _userManager.FindByNameAsync(username);

           
            var jobModel = (await _jobRepo.GetAsync()).FirstOrDefault(x => x.Id == job.Id);
            if(jobModel == null)
            {
               
                var createdJob = await _jobRepo.CreateAsync(job);
                

            }
            var userFavorites = await _favoriteRepo.GetUserFavoriteAsync(appUser);


            var existingFavorite = userFavorites.FirstOrDefault(x => x.Id == job.Id);
            if (existingFavorite != null)
            {
                TempData["ErrorMessage"] = $"Вакансия {job.Title} уже находится в избранном.";
                return RedirectToAction("GetAll", "Job");
            }
            var favoriteModel = new Favorite
            {   

                JobId = job.Id,
                AppUserId = appUser.Id

            };
            await _favoriteRepo.CreateAsync(favoriteModel);
            if (favoriteModel == null)
            {
                return StatusCode(500,"Dont created");
            }
            return Created();
        }


        [Authorize]
        [HttpGet("GetFavorites")]
        public async Task<IActionResult> Favorites()
        {
            var userName =  User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            var appUser = await _userManager.FindByNameAsync(userName);
            var userFavorite = await _favoriteRepo.GetUserFavoriteAsync(appUser);
            return View(userFavorite);
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteJobFromFavories([FromForm]int id)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            var appUser = await _userManager.FindByNameAsync(userName);
            var userFavorite = await _favoriteRepo.DeleteJobFromFavorite(id);

            return RedirectToAction("Favorites");  
        }

        [HttpGet("searchInFavorites")]
        public async Task<IActionResult> Search(string? searchString)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            var appUser = await _userManager.FindByNameAsync(userName);
            var jobs = await _favoriteRepo.GetUserFavoriteAsync(appUser);

            if (searchString == null)
            {
                return View("Favorites", jobs);
            }

            if (jobs == null || !jobs.Any())
            {
                ViewData["CurrentFilter"] = searchString;
                return View("GetAll", new List<Job>());
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(j =>
                    j.Title.ToLower().Contains(searchString.ToLower()) ||
                    j.CompanyName.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            ViewData["CurrentFilter"] = searchString;

            return View("Favorites", jobs);
        }

    }
}
