using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Client.ViewModels.Basket;
using JavidElectronics.Database;
using JavidElectronics.Services.Abstracts;
using System.Text.Json;

namespace JavidElectronics.Areas.Client.ViewComponents.Basket
{
    [ViewComponent(Name = "MiniBasket")]
    public class MiniBasket : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public MiniBasket(DataContext dataContext, IUserService userService = null, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<BasketCookieViewModel>? viewModels = null)
        {
            // Case 1 : Logined, data from Database
            if (_userService.IsAuthenticated)
            {
                var model = await _dataContext.BasketProducts
                    .Where(p => p.Basket.UserId == _userService.CurrentUser.Id)
                   .Select(p =>
                   new BasketCookieViewModel(
                       p.ProductId,
                       p.Product.Name,
                       p.Product.ProductImages.Take(1).FirstOrDefault()! != null
                   ? _fileService.GetFileUrl(p.Product.ProductImages.Take(1).FirstOrDefault().ImageNameInFileSystem, 
                   Contracts.File.UploadDirectory.Product)
                   : String.Empty,
                   p.Quantity,
                   p.Product.Price.Value,
                   p.Product.Price.Value * p.Quantity))
                   .ToListAsync();


                return View(model);
            }

            //Case 2: Argument is sended from action
            if (viewModels is not null)
            {
                return View(viewModels);
            }

            //Case 3: Argument is not sended, read from cookie
            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<BasketCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }
    }
}
