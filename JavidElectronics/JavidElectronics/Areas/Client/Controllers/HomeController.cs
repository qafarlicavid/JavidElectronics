using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Areas.Client.ViewModels.Home.Contact;
using JavidElectronics.Services.Abstracts;
using JavidElectronics.Contracts.File;
using JavidElectronics.Areas.Client.ViewModels.Home.Index;
using JavidElectronics.Areas.Client.ViewModels.Home.Index.Modal;

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

        [HttpGet("GetModel/{id}", Name = "Product-GetModel")]
        public async Task<ActionResult> GetModelAsync(int id)
        {
            var product = await _dbContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductColors).FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }

            var model = new ModalViewModel(product.Name, product.Description, product.Price.Value,
                product.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                : String.Empty,

                _dbContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                .Select(pc => new ModalViewModel.ColorViewModel(pc.Color.Name, pc.Color.Id)).ToList()

                );

            return PartialView("~/Areas/Client/Views/Shared/Partials/_ProductModelPartial.cshtml", model);
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
