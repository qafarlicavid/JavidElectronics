namespace JavidElectronics.Areas.Admin.ViewModels.Blog
{
    public class AddViewModel
    {
        public AddViewModel(string title, string content,string from, List<TagListItemViewModel>? tags, List<int> tagIds, List<CategoryListItemViewModel>? categories, List<int> categoryIds)
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
        public List<TagListItemViewModel>? Tags { get; set; }
        public List<CategoryListItemViewModel>? Categories { get; set; }

        public List<int> TagIds { get; set; }
        public List<int> CategoryIds { get; set; }

    }
}
