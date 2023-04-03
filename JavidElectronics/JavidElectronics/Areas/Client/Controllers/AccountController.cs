using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JavidElectronics.Database;
using JavidElectronics.Services.Abstracts;

namespace JavidElectronics.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        //[HttpGet("order", Name = "client-account-order")]
        //public async  Task<IActionResult> Order()
        //{
        //    var model = await _dataContext.Orders
        //        .Select(b => new OrderViewModel(b.Id, b.CreatedAt, b.Status, b.SumTotalPrice))
        //        .ToListAsync();

        //    return View(model);
        //}

        [HttpGet("address", Name = "client-account-address")]
        public IActionResult Address()
        {
            return View();
        }

        [HttpGet("details", Name = "client-account-details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}