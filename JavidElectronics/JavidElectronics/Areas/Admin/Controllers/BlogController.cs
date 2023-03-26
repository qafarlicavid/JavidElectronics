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
                          new ListViewModel(b.Id, b.Title!, b.Content!, b.AdminId, b.Admin!.FirstName,
                           b.BlogImages!.Take(1)!.FirstOrDefault()! != null
                           ? _fileService.GetFileUrl(b.BlogImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.BlogImage)
                           : string.Empty,
                           b.BlogTags!.Select(pt => new ViewModels.Tag.ListItemViewModel(pt.Tag!.Id, pt.Tag.Title)).ToList()
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
                    .Select(t => new ViewModels.Tag.ListItemViewModel(t.Id, t.Title))
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


            var blog = await AddBlogAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");



            async Task<IActionResult> GetViewAsync(AddViewModel model)
            {
                model.Tags = await _dataContext.Tags
                    .Select(t => new ViewModels.Tag.ListItemViewModel(t.Id, t.Title))
                    .ToListAsync();


                return View(model);
            }

            async Task<Blog> AddBlogAsync()
            {
                var blog = new Blog
                {
                    Title = model.Title,
                    Content = model.Content,
                    Admin = _userService.CurrentUser,
                    AdminId = _userService.CurrentUser.Id,


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
                    Tags = await _dataContext.Tags
                                        .Select(t => new ViewModels.Tag.ListItemViewModel(t.Id, t.Title))
                                        .ToListAsync(),

                    TagIds = blog!.BlogTags!.Select(pt => pt.TagId).ToList(),

                };
                return model;
            };
        }


        [HttpPost("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var blog = await _dataContext.Blogs
                .Include(p => p.BlogTags)
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




            await UpdateBlogAsync();

            return RedirectToRoute("admin-blog-list");




            async Task<IActionResult> GetViewAsync(UpdateViewModel model)
            {

                model.Tags = await _dataContext.Tags
                 .Select(t => new ViewModels.Tag.ListItemViewModel(t.Id, t.Title))
                 .ToListAsync();

                model.TagIds = blog!.BlogTags!.Select(pt => pt.TagId).ToList();


                return View(model);
            }

            async Task UpdateBlogAsync()
            {
                blog.Title = model.Title;
                blog.Content = model.Content;


                await UpdateBlogTag();

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
