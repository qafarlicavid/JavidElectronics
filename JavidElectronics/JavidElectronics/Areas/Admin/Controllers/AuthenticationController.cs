using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Admin.ViewModels.Authentication;
using JavidElectronics.Contracts.Identity;
using JavidElectronics.Database;
using JavidElectronics.Services.Abstracts;

namespace JavidElectronics.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/auth")]
    public class AuthenticationController : Controller
    {

        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        [HttpGet("login", Name = "admin-auth-login")]
        public async Task<IActionResult> Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "admin-auth-login")]
        public async Task<IActionResult> Login(LoginViewModel? model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(string.Empty, "Email or password is not correct");
                return View(model);
            }

            if (!await _dbContext.Users.AnyAsync(u => u.Role.Name == RoleNames.ADMIN))
            {
                ModelState.AddModelError(string.Empty, "Role is not Admin");
                return View(model);
            }

            if (!await _dbContext.Users.AnyAsync(u => u.Email == model.Email && u.Role.Name == RoleNames.ADMIN))
            {
                ModelState.AddModelError(string.Empty, "Role is not Admin");
                return View(model);
            }

            return RedirectToRoute("admin-product-list");

        }

        [HttpGet("logout", Name = "admin-auth-logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("admin-auth-login");
        }
    }
}