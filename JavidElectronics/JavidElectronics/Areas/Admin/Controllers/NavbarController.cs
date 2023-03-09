using JavidElectronics.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Database.Models;
using Microsoft.AspNetCore.Authorization;
using JavidElectronics.Areas.Admin.ViewModels.Navbar;

namespace JavidElectronics.Controllers.Admin
{
    [Area("admin")]
    [Route("admin/navbar")]
    //[Authorize(Roles = "admin")]
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List

        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()

        {
            var model = await _dataContext.Navbars
                .Select(a => new ListItemViewModel(a.Id, a.Name!, a.ToURL!, a.Order, a.IsOnHeader, a.IsOnFooter))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var navbar = new Navbar
            {
                Id = model.Id,
                Name = model.Name,
                ToURL = model.ToURL,
                Order = model.Order,
                IsMain = model.IsMain,
                IsOnHeader = model.IsOnHeader,
                IsOnFooter = model.IsOnFooter
            };
            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");

        }


        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Name = navbar.Name!,
                ToURL = navbar.ToURL!,
                Order = navbar.Order,
                IsMain = navbar.IsMain,
                IsOnHeader = navbar.IsOnHeader,
                IsOnFooter = navbar.IsOnFooter
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navbar is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }


            navbar.Name = model.Name;
            navbar.ToURL = model.ToURL;
            navbar.Order = model.Order;
            navbar.IsOnHeader = model.IsOnHeader;
            navbar.IsOnFooter = model.IsOnFooter;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-navbar-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(navbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }

        #endregion
    }
}