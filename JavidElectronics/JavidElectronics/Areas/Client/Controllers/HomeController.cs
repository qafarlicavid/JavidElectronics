using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Areas.Client.ViewModels.Home.Contact;

namespace JavidElectronics.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        public HomeController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("index", Name = "client-home-index")]
        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }

        #region Contact

        [HttpGet("contact")]
        public async Task<IActionResult> ContactAsync()
        {
            return View();
        }

        [HttpPost("contact", Name = "client-home-index")]
        public async Task<IActionResult> ContactAsync([FromForm] CreateViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _dbContext.Contacts.AddAsync(new Contact
            {
                Name = contactViewModel.Name,
                Email = contactViewModel.Email,
                Message = contactViewModel.Message,
                Phone = contactViewModel.Phone,
                CreatedAt = DateTime.Now
            });

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(IndexAsync));
        }

        #endregion
    }
}
