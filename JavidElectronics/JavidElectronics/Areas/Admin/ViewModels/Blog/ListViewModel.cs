namespace JavidElectronics.Areas.Admin.ViewModels.Blog
{
    public class ListViewModel
    {
        public ListViewModel(int id, string title, string content,string from, Guid? adminId, string? adminName, string videoURL, List<Tag.ListItemViewModel>? tags, List<Category.ListItemViewModel>? categories)
        {
            Id = id;
            Title = title;
            Content = content;
            From = from;
            AdminId = adminId;
            AdminName = adminName;
            VideoURL = videoURL;
            Tags = tags;
            Categories = categories;
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string From { get; set; } = null!;
        public Guid? AdminId { get; set; }
        public string? AdminName { get; set; }

        public string VideoURL { get; set; }
        public List<ViewModels.Tag.ListItemViewModel>? Tags { get; set; }
        public List<ViewModels.Category.ListItemViewModel>? Categories { get; set; }
    }
}
