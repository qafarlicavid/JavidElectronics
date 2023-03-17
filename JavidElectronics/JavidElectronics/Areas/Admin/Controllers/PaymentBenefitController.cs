using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Admin.ViewModels.PaymentBenefit;
using JavidElectronics.Contracts.File;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Services.Abstracts;

namespace JavidElectronics.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/paymentbenefit")]
    public class PaymentBenefitController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentBenefitController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("paymentbenefit", Name = "admin-paymentbenefit-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.PaymentBenefits.Select(p => new ListItemViewModel(p.Id, p.Name!, p.Content!, _fileService
                 .GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.PaymentBenefit))).ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-paymentbenefit-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-paymentbenefit-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.ImageName!, UploadDirectory.PaymentBenefit);

            var benefit = new PaymentBenefit
            {
                Name = model.Name,
                Content = model.Content,
                ImageName = model.ImageName!.FileName,
                ImageNameInFileSystem = imageNameInSystem,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _dataContext.PaymentBenefits.AddAsync(benefit);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-paymentbenefit-list");
        }


        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-paymentbenefit-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var benefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b => b.Id == id);

            if (benefit is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = benefit.Id,
                Name = benefit.Name,
                Content = benefit.Content,
                ImageUrl = _fileService.GetFileUrl(benefit.ImageNameInFileSystem, UploadDirectory.PaymentBenefit),
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-paymentbenefit-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var benefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (benefit is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _fileService.DeleteAsync(benefit.ImageNameInFileSystem, UploadDirectory.PaymentBenefit);
            var imageNameInSystem = await _fileService.UploadAsync(model.ImageName!, UploadDirectory.PaymentBenefit);

            benefit.Name = model.Name;
            benefit.Content = model.Content;
            benefit.ImageName = model.ImageName!.FileName;
            benefit.ImageNameInFileSystem = imageNameInSystem;



            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-paymentbenefit-list");
        }
        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-paymentbenefit-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var paymentbenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b => b.Id == id);
            if (paymentbenefit is null)
            {
                return NotFound();
            }

            _dataContext.PaymentBenefits.Remove(paymentbenefit);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-paymentbenefit" +
                "-list");
        }

        #endregion

    }
}
