using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Admin.ViewModels.Blog;
using JavidElectronics.Contracts.File;
using JavidElectronics.Contracts.Identity;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Services.Abstracts;

namespace JavidElectronics.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("blog")]
    //[Authorize(Roles = RoleNames.ADMIN)]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<BlogController> _logger;
        private readonly IUserService _userService;
        private User _CurrentUser;


        public BlogController
            (DataContext dataContext,
            IFileService fileService,
            ILogger<BlogController> logger,
            IUserService userService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _userService = userService;
            _logger = logger;
        }
        #region List
        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await CreateModelAsync();

            return View(model);


            async Task<List<ListViewModel>> CreateModelAsync()
            {
                var model = await _dataContext.Blogs
                      .Select(b =>
                          new ListViewModel(b.Id, b.Title!, b.Content!,b.From!, b.AdminId, b.Admin!.FirstName,
                           b.BlogImages!.Take(1)!.FirstOrDefault()! != null
                           ? _fileService.GetFileUrl(b.BlogImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.BlogImage)
                           : string.Empty,
                           b.BlogTags!.Select(pt => new TagListItemViewModel(pt.Tag!.Id, pt.Tag.Title)).ToList(),
                           b.BlogCategories!.Select(bc => new CategoryListItemViewModel(bc.Category!.Id, bc.Category.Title!))
                           .ToList()
                      ))


                          .ToListAsync();
                return model;
            }
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Tags = await _dataContext.Tags
                    .Select(t => new TagListItemViewModel(t.Id, t.Title))
                    .ToListAsync(),
                Categories = await _dataContext.Categories
                    .Select(t => new CategoryListItemViewModel(t.Id, t.Title))
                    .ToListAsync(),
            };

            return View(model);
        }
        [HttpPost("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return await GetViewAsync(model);


            foreach (var tagId in model.TagIds)
            {
                if (!_dataContext.Tags.Any(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"tag with id({tagId}) not found in db ");
                    return await GetViewAsync(model);
                }
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!_dataContext.Categories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"category with id({categoryId}) not found in db ");
                    return await GetViewAsync(model);
                }
            }


            var blog = await AddBlogAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");



            async Task<IActionResult> GetViewAsync(AddViewModel model)
            {
                model.Tags = await _dataContext.Tags
                    .Select(t => new TagListItemViewModel(t.Id, t.Title))
                    .ToListAsync();
                model.Categories = await _dataContext.Categories
                    .Select(t => new CategoryListItemViewModel(t.Id, t.Title))
                    .ToListAsync();

                return View(model);
            }

            async Task<Blog> AddBlogAsync()
            {
                var blog = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    From = model.From,
                    AdminId = _userService.CurrentUser.Id,
                    Admin = _userService.CurrentUser,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _dataContext.Blogs.AddAsync(blog);


                foreach (var tagId in model.TagIds)
                {
                    var blogTag = new BlogTag
                    {
                        TagId = tagId,
                        Blog = blog,
                    };

                    await _dataContext.BlogTags.AddAsync(blogTag);
                }

                foreach (var categoryId in model.CategoryIds)
                {
                    var blogCategory = new BlogCategory
                    {
                        CategoryId = categoryId,
                        Blog = blog,
                    };

                    await _dataContext.BlogCategories.AddAsync(blogCategory);
                }

                return blog;
            }
        }
        #endregion


        #region Update
        [HttpGet("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var blog = await _dataContext.Blogs
                .Include(p => p.BlogTags)
                .Include(p => p.BlogCategories)
                .FirstOrDefaultAsync(b => b.Id == id);


            if (blog is null) return NotFound();


            var model = await CreateModelAsync();


            return View(model);

            async Task<UpdateViewModel> CreateModelAsync()
            {
                var model = new UpdateViewModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Content = blog.Content,
                    From = blog.From,
                    Tags = await _dataContext.Tags
                                        .Select(t => new TagListItemViewModel(t.Id, t.Title))
                                        .ToListAsync(),

                    TagIds = blog!.BlogTags!.Select(pt => pt.TagId).ToList(),

                    Categories = await _dataContext.Categories
                                        .Select(t => new CategoryListItemViewModel(t.Id, t.Title))
                                        .ToListAsync(),

                    CategoryIds = blog!.BlogCategories!.Select(pt => pt.CategoryId).ToList(),

                };
                return model;
            };
        }


        [HttpPost("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var blog = await _dataContext.Blogs
                .Include(p => p.BlogTags)
                .Include(p => p.BlogCategories)
                .FirstOrDefaultAsync(p => p.Id == model.Id);


            if (blog is null) return NotFound();

            if (!ModelState.IsValid) return await GetViewAsync(model);



            foreach (var tagId in model.TagIds)
            {
                if (!_dataContext.Tags.Any(t => t.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"tag with id({tagId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }


            foreach (var categoryId in model.CategoryIds)
            {
                if (!_dataContext.Categories.Any(t => t.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"tag with id({categoryId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }


            await UpdateBlogAsync();

            return RedirectToRoute("admin-blog-list");




            async Task<IActionResult> GetViewAsync(UpdateViewModel model)
            {

                model.Tags = await _dataContext.Tags
                 .Select(t => new TagListItemViewModel(t.Id, t.Title))
                 .ToListAsync();

                model.TagIds = blog!.BlogTags!.Select(pt => pt.TagId).ToList();

                model.Categories = await _dataContext.Categories
                 .Select(t => new CategoryListItemViewModel(t.Id, t.Title))
                 .ToListAsync();

                model.CategoryIds = blog!.BlogCategories!.Select(pt => pt.CategoryId).ToList();


                return View(model);
            }

            async Task UpdateBlogAsync()
            {
                blog.Title = model.Title;
                blog.Content = model.Content;
                blog.From = model.From;


                await UpdateBlogTag();
                await UpdateBlogCategory();

                await _dataContext.SaveChangesAsync();
            }

            async Task UpdateBlogTag()
            {
                var TagsInDb = blog.BlogTags.Select(pt => pt.TagId).ToList();
                var TagsInDbToRemove = TagsInDb.Except(model.TagIds).ToList();
                var TagsToAdd = model.TagIds.Except(TagsInDb).ToList();

                blog.BlogTags.RemoveAll(pt => TagsInDbToRemove.Contains(pt.TagId));

                foreach (var tagId in TagsToAdd)
                {
                    var blogTag = new BlogTag
                    {
                        TagId = tagId,
                        Blog = blog,
                    };

                    await _dataContext.BlogTags.AddAsync(blogTag);
                }
            }
            async Task UpdateBlogCategory()
            {
                var CategoriesInDb = blog.BlogCategories.Select(pt => pt.BlogId).ToList();
                var CategoriesInDbToRemove = CategoriesInDb.Except(model.CategoryIds).ToList();
                var CategoriesToAdd = model.CategoryIds.Except(CategoriesInDb).ToList();

                blog.BlogCategories.RemoveAll(pt => CategoriesInDbToRemove.Contains(pt.CategoryId));

                foreach (var categoryId in CategoriesToAdd)
                {
                    var blogCategory = new BlogCategory
                    {
                        CategoryId = categoryId,
                        Blog = blog,
                    };

                    await _dataContext.BlogCategories.AddAsync(blogCategory);
                }
            }





        }
        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var blog = await _dataContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);


            if (blog is null) return NotFound();


            _dataContext.Blogs.Remove(blog);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");
        }

        #endregion

    }
}
