using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Admin.ViewModels.BlogImage;
using JavidElectronics.Contracts.File;
using JavidElectronics.Contracts.Identity;
using JavidElectronics.Database;
using JavidElectronics.Database.Models;
using JavidElectronics.Services.Abstracts;

namespace JavidElectronics.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogs")]
    //[Authorize(Roles = RoleNames.ADMIN)]
    public class BlogImageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogImageController(
            DataContext dataContext,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("{blogId}/image/list", Name = "admin-blog-image-list")]
        public async Task<IActionResult> ListAsync([FromRoute] int blogId)
        {
            var blog = await _dataContext.Blogs
                .Include(p => p.BlogImages)
                .FirstOrDefaultAsync(p => p.Id == blogId);



            if (blog is null) return NotFound();

            var model = new BlogImagesViewModel { BlogId = blog.Id };

            model.Images = blog.BlogImages!.Select(pi => new BlogImagesViewModel.ListItem
            {
                Id = pi.Id,
                ImageUrL = _fileService.GetFileUrl(pi.ImageNameInFileSystem, UploadDirectory.BlogImage),

            }).ToList();

            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("{blogId}/image/add", Name = "admin-blog-image-add")]
        public IActionResult Add()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{blogId}/image/add", Name = "admin-blog-image-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int blogId, [FromForm] AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var blog = await _dataContext.Blogs.FirstOrDefaultAsync(b => b.Id == blogId);


            if (blog is null) return NotFound();

            var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.BlogImage);

            var blogImage = CreateBlogImage();

            await _dataContext.BlogImages.AddAsync(blogImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-image-list", new { blogId = blogId });


            BlogImage CreateBlogImage()
            {
                var blogImage = new BlogImage
                {
                    Blog = blog,
                    BlogId = blogId,
                    ImageName = model.Image.FileName,
                    ImageNameInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                return blogImage;
            }
        }

        #endregion

        #region Update
        [HttpGet("{blogId}/image/{blogImageId}/update", Name = "admin-blog-image-update")]
        public async Task<IActionResult> UpdateAsync(int blogId, int blogImageId)
        {
            var blogImage = await _dataContext.BlogImages
               .FirstOrDefaultAsync(bi => bi.Id == blogImageId && bi.BlogId == blogId);


            if (blogImage is null) return NotFound();

            var model = new UpdateViewModel
            {
                ImageUrL = _fileService.GetFileUrl(blogImage.ImageNameInFileSystem, UploadDirectory.BlogImage),
            };

            return View(model);
        }
        [HttpPost("{blogId}/image/{blogImageId}/update", Name = "admin-blog-image-update")]
        public async Task<IActionResult> UpdateAsync(int blogId, int blogImageId, [FromForm] UpdateViewModel model)
        {
            var blogImage = await _dataContext.BlogImages
                .FirstOrDefaultAsync(bi => bi.Id == blogImageId && bi.BlogId == blogId);




            if (blogImage is null) return NotFound();





            if (!(model.Image == null)) await UpdateImageAsync();








            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-image-update", new { blogId = blogId });

            async Task UpdateImageAsync()
            {
                await _fileService.DeleteAsync(blogImage.ImageNameInFileSystem, UploadDirectory.BlogImage);

                var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.BlogImage);
                blogImage.ImageNameInFileSystem = imageNameInSystem;
                blogImage.ImageName = model.Image.FileName;
            };
        }

        #endregion

        #region Delete

        [HttpPost("{blogId}/image/{blogImageId}/delete", Name = "admin-blog-image-delete")]
        public async Task<IActionResult> DeleteAsync(int blogId, int blogImageId)
        {
            var blogImage = await _dataContext.BlogImages
                .FirstOrDefaultAsync(bi => bi.Id == blogImageId && bi.BlogId == blogId);


            if (blogImage is null) return NotFound();

            await _fileService.DeleteAsync(blogImage.ImageNameInFileSystem, UploadDirectory.BlogImage);

            _dataContext.BlogImages.Remove(blogImage);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-image-list", new { blogId = blogId });
        }

        #endregion

    }
}
