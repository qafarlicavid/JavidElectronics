using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JavidElectronics.Areas.Client.ViewModels.Blog;
using JavidElectronics.Contracts.File;
using JavidElectronics.Database;
using JavidElectronics.Services.Abstracts;
using JavidElectronics.Areas.Client.ViewModels.Home.Index;

namespace JavidElectronics.Areas.Client.ViewComponents.Blog
{
    public class Blog : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Blog(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? tagId = null, string? searchQuery = null)
        {
            var blogsQuery = _dataContext.Blogs.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                blogsQuery = blogsQuery
                    .Where(p => p.Title!.StartsWith(searchQuery))
                    .Take(8);
            }
            else if (tagId != null)
            {
                blogsQuery = blogsQuery
                  .Where(p => p.BlogTags.Any(pt => pt.TagId == tagId))
                  .Take(8);
            }
            else
            {
                blogsQuery = blogsQuery
                    .Take(8);
            }
          
            var model = new IndexViewModel
            {
                Blogs = await _dataContext.Blogs.Select(p => new BlogListItemViewModel(
                  p.Id,
                  p.Title,

                  p.BlogImages!.Take(1)!.FirstOrDefault()! != null
                       ? _fileService.GetFileUrl(p.BlogImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.BlogImage)
                       : string.Empty,

                  p.BlogImages!.Skip(1).Take(1)!.FirstOrDefault()! != null
                       ? _fileService.GetFileUrl(p.BlogImages!.Skip(1)!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.BlogImage)
                       : string.Empty,

                   p.BlogVideos!.Take(1)!.FirstOrDefault()! != null
                       ? p.BlogVideos!.Take(1)!.FirstOrDefault()!.VideoURLFromBrowser!
                       : string.Empty,

                   p.Admin!.FirstName!,
                   p.CreatedAt,
                   p.Content
                   ))
                .ToListAsync()
            };


            return View(model);
        }
    }
}
