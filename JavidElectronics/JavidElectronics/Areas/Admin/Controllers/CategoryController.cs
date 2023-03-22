using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Admin.ViewModels.Category;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;

namespace JavidElectronics.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        [HttpGet("list", Name = "admin-category-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Categories.Select(s => new ListItemViewModel(s.Id, s.Title!)).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-category-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-category-add")]
        public async Task<IActionResult> Add(ListItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };


            await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-category-list");
        }
        [HttpGet("update/{id}", Name = "admin-category-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(n => n.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            var model = new ListItemViewModel
            {
                Id = category.Id,
                Title = category.Title!,

            };

            return View(model);
        }


        [HttpPost("update/{id}", Name = "admin-category-update")]
        public async Task<IActionResult> UpdateAsync(ListItemViewModel model)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (category is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            category.Title = model.Title;


            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-category-list");
        }



        [HttpPost("delete/{id}", Name = "admin-category-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(n => n.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            _dataContext.Categories.Remove(category);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-category-list");
        }
    }
}
