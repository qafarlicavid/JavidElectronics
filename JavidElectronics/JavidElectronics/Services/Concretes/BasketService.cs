using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Client.ViewModels.Basket;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Services.Abstracts;
using System.Text.Json;

namespace JavidElectronics.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public BasketService(DataContext dataContext, IUserService userService, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product)
        {
            if (_userService.IsAuthenticated)
            {
                await AddToDatabaseAsync();
                return new List<BasketCookieViewModel>();
            }

            return AddCookie();




            async Task AddToDatabaseAsync()
            {
                var basketProduct = await _dataContext.BasketProducts
                     .Include(b => b.Basket)
                     .FirstOrDefaultAsync(bp => bp.Basket.User.Id == _userService.CurrentUser.Id && bp.ProductId == product.Id);

                if (basketProduct is not null)
                {
                    basketProduct.Quantity++;
                }
                else
                {
                    var basket = await _dataContext.Baskets.FirstOrDefaultAsync(p => p.UserId == _userService.CurrentUser.Id);

                    basketProduct = new BasketProduct
                    {
                        Quantity = 1,
                        BasketId = basket.Id,
                        ProductId = product.Id
                    };

                    await _dataContext.BasketProducts.AddAsync(basketProduct);
                }
                await _dataContext.SaveChangesAsync();
            }

            List<BasketCookieViewModel> AddCookie()
            {
                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["products"];

                var productCookieViewModel = productCookieValue is not null
                   ? JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue)
                   : new List<BasketCookieViewModel> { };

                var cookieViewModel = productCookieViewModel!.FirstOrDefault(pc => pc.Id == product.Id);
                if (cookieViewModel is null)
                {
                    productCookieViewModel.Add
                        (new BasketCookieViewModel(product.Id, product.Name, product.ProductImages.Take(1).FirstOrDefault() != null
                        ? _fileService.GetFileUrl(product.ProductImages.Take(1).FirstOrDefault().ImageNameInFileSystem, Contracts.File.UploadDirectory.Product)
                        : String.Empty, 1, product.Price.Value, product.Price.Value));
                }
                else
                {
                    cookieViewModel.Quantity += 1;
                    cookieViewModel.Total = cookieViewModel.Quantity * cookieViewModel.Price;
                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));

                return productCookieViewModel;
            };
        }
    }
}
