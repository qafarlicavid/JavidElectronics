namespace JavidElectronics.Areas.Admin.ViewModels.Blog
{
    public class AddViewModel
    {
        public AddViewModel(string title, string content, List<Tag.ListItemViewModel>? tags, List<int> tagIds)
        {
            Title = title;
            Content = content;
            Tags = tags;
            TagIds = tagIds;
        }
        public AddViewModel()
        {

        }

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public List<ViewModels.Tag.ListItemViewModel>? Tags { get; set; }
        public List<int> TagIds { get; set; }

    }
}
