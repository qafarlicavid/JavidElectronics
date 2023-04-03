using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Client.ViewComponents.Blog;
using JavidElectronics.Areas.Client.ViewModels.Blog;
using JavidElectronics.Database;

namespace JavidElectronics.Areas.Client.Controllers
{

    [Area("client")]
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        public BlogController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("index", Name = "client-blog-index")]
        public async Task<IActionResult> IndexAsync(string? searchQuery = null)
        {
            var model = await CreateModel();



            return View(model);



            async Task<ListViewModel> CreateModel()
            {
                var model = new ListViewModel
                {
                    SearchQuery = searchQuery,
                    Tags = await _dataContext.Tags.Select(t => new JavidElectronics.Areas.Admin.ViewModels.Tag.ListItemViewModel(t.Id, t.Title)).ToListAsync()

                };
                return model;
            }
        }
        [HttpGet("search", Name = "client-blog-search")]
        public IActionResult Search(ListViewModel model)
        {
            return ViewComponent(nameof(Blog),
                 new
                 {
                     slide = string.Empty,
                     tagId = model.TagId,
                     searchQuery = model.SearchQuery

                 });
        }
    }
}
