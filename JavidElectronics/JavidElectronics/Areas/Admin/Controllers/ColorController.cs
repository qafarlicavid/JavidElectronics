using JavidElectronics.Areas.Admin.ViewModels.Color;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JavidElectronics.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/color")]
    //[Authorize(Roles = "admin")]
    public class ColorController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public ColorController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List
        [HttpGet("list", Name = "admin-color-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Colors
                .Select(c => new ListItemViewModel(c.Id, c.Name))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-color-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-color-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var color = new Color
            {

                Name = model.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,


            };
            await _dataContext.Colors.AddAsync(color);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-color-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-color-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(n => n.Id == id);


            if (color is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                Name = color.Name,


            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-color-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (color is null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            if (!_dataContext.Colors.Any(n => n.Id == model.Id)) return View(model);

            color.Name = model.Name;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-color-list");

        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-color-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (color is null) return NotFound();

            _dataContext.Colors.Remove(color);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-color-list");

        }
        #endregion
    }
}