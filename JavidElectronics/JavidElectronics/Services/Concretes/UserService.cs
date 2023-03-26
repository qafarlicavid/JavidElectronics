using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Client.ViewModels.Authentication;
//using JavidElectronics.Areas.Client.ViewModels.Basket;
using JavidElectronics.Contracts.Identity;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Exceptions;
using JavidElectronics.Services.Abstracts;
using System.Security.Claims;
using System.Text.Json;


namespace JavidElectronics.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserActivationService _userActivationService;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IUserActivationService userActivationService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userActivationService = userActivationService;
        }


        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }
                var idClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaimNames.ID);

                if (idClaim is null)
                {
                    throw new IdentityCookieException("Identity cookie not found");
                }
                _currentUser = _dataContext.Users.First(u => u.Id == Guid.Parse(idClaim.Value));

                return _currentUser;
            }
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor!.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            var model = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (model is null || !BCrypt.Net.BCrypt.Verify(password, model.Password))
            {
                return false;
            }
            return true;
        }


        public async Task SignInAsync(Guid id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID,id.ToString())
            };
            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            var user = await _dataContext.Users.FirstAsync(u => u.Email == email);

            if (user is not null && BCrypt.Net.BCrypt.Verify(password, user.Password) && user.IsEmailConfirmed == true)
            {
                await SignInAsync(user.Id, role);
            }
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            var user = await CreateUser();
            var basket = await CreateBasket();

            //await CreateBasketProduct();

            await _userActivationService.SendActivationUrlAsync(user);


            await _dataContext.SaveChangesAsync();




            async Task<User> CreateUser()
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    RoleId = 5,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                await _dataContext.Users.AddAsync(user);

                return user;
            }
            async Task<Basket> CreateBasket()
            {
                var basket = new Basket
                {
                    User = user,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                };
                await _dataContext.Baskets.AddAsync(basket);
                return basket;
            };

            //async Task CreateBasketProduct()
            //{
            //    var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["products"];
            //    if (productCookieValue is not null)
            //    {
            //        var productsCookieViewModel = JsonSerializer.Deserialize<List<BasketCookieViewModel>>(productCookieValue);

            //        foreach (var cookieViewModel in productsCookieViewModel)
            //        {
            //            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == cookieViewModel.Id);

            //            var basketProduct = new BasketProduct
            //            {
            //                Basket = basket,
            //                ProductId = product.Id,
            //                Quantity = cookieViewModel.Quantity
            //            };

            //            await _dataContext.BasketProducts.AddAsync(basketProduct);
            //        }
            //    }
            //};

        }


    }
}