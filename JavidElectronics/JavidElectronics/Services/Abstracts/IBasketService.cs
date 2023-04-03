using JavidElectronics.Areas.Client.ViewModels.Basket;
using JavidElectronics.Database.Models;

namespace JavidElectronics.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<BasketCookieViewModel>> AddBasketProductAsync(Product product);
    }
}
