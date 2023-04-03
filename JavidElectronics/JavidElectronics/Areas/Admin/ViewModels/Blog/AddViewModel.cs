namespace JavidElectronics.Areas.Admin.ViewModels.Blog
{
    public class AddViewModel
    {
        public AddViewModel(string title, string content,string from, List<Tag.ListItemViewModel>? tags, List<int> tagIds, List<Category.ListItemViewModel>? categories, List<int> categoryIds)
        {
            Title = title;
            Content = content;
            From = from;
            Tags = tags;
            TagIds = tagIds;
            Categories = categories;
            CategoryIds = categoryIds;
        }
        public AddViewModel()
        {

        }

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string From { get; set; } = null!;
        public List<ViewModels.Tag.ListItemViewModel>? Tags { get; set; }
        public List<ViewModels.Category.ListItemViewModel>? Categories { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> CategoryIds { get; set; }

    }
}
