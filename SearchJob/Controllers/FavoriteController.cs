using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchJob.Data;
using SearchJob.Interfaces;
using SearchJob.Models;
using SearchJob.Repository;
using System.Security.Claims;

namespace SearchJob.Controllers
{
    [ApiController]
    [Route("api/favorites")]
    public class FavoriteController:ControllerBase 
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
        [HttpPost]
        public async Task<IActionResult> AddFavorite(string title)
        {   
           
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var appUser = await _userManager.FindByNameAsync(username);
            var job = (await _jobRepo.GetAsync()).FirstOrDefault(x => x.Title == title);


            
            if (job == null)
            {
                job = await _hhService.FindJobByTitleAsync(title);
                if (job == null)
                {
                    return BadRequest("Job does not exists");
                }
                else
                {
                    await _jobRepo.CreateAsync(job);
                }
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
        [HttpGet]
        public async Task<IActionResult> GetUserFavorites()
        {
            var userName =  User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            var appUser = await _userManager.FindByNameAsync(userName);
            var userFavorite = await _favoriteRepo.GetUserFavorite(appUser);
            return Ok(userFavorite);
        }

    }
}
