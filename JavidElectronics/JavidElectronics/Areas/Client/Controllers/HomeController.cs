using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Areas.Client.ViewModels.Home.Contact;
using JavidElectronics.Services.Abstracts;
using JavidElectronics.Contracts.File;
using JavidElectronics.Areas.Client.ViewModels.Home.Index;

namespace JavidElectronics.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync([FromServices] IFileService fileService)
        {
            var model = new IndexViewModel
            {
                Sliders = await _dbContext.Sliders.OrderBy(s => s.Order).Select(s => new SliderListItemViewModel(s.Id, s.MainTitle!, s.Content!,
                s.Button!,
                s.ButtonRedirectUrl!,
                fileService.GetFileUrl(s.BackgroundİmageInFileSystem, UploadDirectory.Slider),
                s.Order))
                .ToListAsync(),
            };



            return View(model);
        }

        #region Contact

        [HttpGet("contact")]
        public async Task<IActionResult> ContactAsync()
        {
            return View();
        }

        [HttpPost("contact", Name = "client-home-contact")]
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
