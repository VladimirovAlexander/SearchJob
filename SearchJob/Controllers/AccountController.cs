using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using NuGet.Protocol;
using SearchJob.Dtos.Account;
using SearchJob.Interfaces;
using SearchJob.Models;
using System.Security.Claims;

namespace SearchJob.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(registerDTO);
                }

                var appUser = new AppUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Пользователь успешно зарегистрирован. Пожалуйста, войдите в систему.";
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Пользователь не зарегистрирован. Пожалуйста, попробуйте еще раз.";
                        return View(registerDTO);
                    }
                }
                else
                {

                    TempData["Errors"] = createdUser.Errors.Select(e => e.Description).ToList();
                    return View(registerDTO);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Произошла ошибка: {ex.Message}");
                TempData["ErrorMessage"] = "Пользователь не зарегистрирован. Пожалуйста, попробуйте еще раз.";
                return View(registerDTO);
            }
        }

        [HttpGet("register")]
        public IActionResult Register()
        
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm]LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded) {


                var token = _tokenService.CreateToken(user);

                
                _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt-token", token);
                
                return Redirect("/api/Job/GetAllJob");
            } 

            ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");
            return View(loginDto);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }



        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        [HttpPut]
        [Authorize]
        public async Task <IActionResult> Update(UpdateAccountRequestDto updateDto)
        {

            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var appUser = await _userManager.FindByNameAsync(username);
            var newToken = _tokenService.CreateToken(appUser);

            var appUserNewEmail = await _userManager.ChangeEmailAsync(appUser, updateDto.Email, newToken);
            
            return View(appUser);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Exit()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt-token");
            return RedirectToAction("Login", "Account");
        }
    }
}
