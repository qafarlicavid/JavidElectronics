using Microsoft.AspNetCore.Mvc;
using JavidElectronics.Areas.Client.ViewModels.Benefit;
using JavidElectronics.Contracts.File;
using JavidElectronics.Database;
using JavidElectronics.Services.Abstracts;

namespace JavidElectronics.Areas.Client.ViewComponents.PaymentBenefit
{
    [ViewComponent(Name = "Benefit")]
    public class Benefit : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public Benefit(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                _dataContext.PaymentBenefits.Select(b => new BenefitListItemViewModel(b.Name!, b.Content!, _fileService
                 .GetFileUrl(b.ImageNameInFileSystem, UploadDirectory.PaymentBenefit))).ToList();

            return View(model);
        }
    }
}
