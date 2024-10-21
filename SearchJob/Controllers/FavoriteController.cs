using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using SearchJob.Data;
using SearchJob.Interfaces;
using SearchJob.Models;
using SearchJob.Repository;
using System.Security.Claims;

namespace SearchJob.Controllers
{
    
    [Route("api/favorites")]
    public class FavoriteController:Controller 
    {
        private readonly UserManager<AppUser> _userManager;
        
        private readonly IFavoriteRepository _favoriteRepo;

        private readonly IJobRepository _jobRepo;

        private readonly IHHService _hhService;
        public FavoriteController(UserManager<AppUser> userManager, IFavoriteRepository favoriteRepo, IJobRepository jobRepo, IHHService hhService) 
        {

            _userManager = userManager;
            _favoriteRepo = favoriteRepo;
            _jobRepo = jobRepo;
            _hhService = hhService;

        }

        [Authorize]
        [HttpPost("AddJobToFavorites")]
        public async Task<IActionResult> AddFavorite([FromForm]int id)
        {   
           
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var appUser = await _userManager.FindByNameAsync(username);
            var job = (await _jobRepo.GetAsync()).FirstOrDefault(x => x.Id == id);
            var userFavorites = await _favoriteRepo.GetUserFavorite(appUser);


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
            var userFavorite = await _favoriteRepo.GetUserFavorite(appUser);
            return View(userFavorite);
        }

    }
}
