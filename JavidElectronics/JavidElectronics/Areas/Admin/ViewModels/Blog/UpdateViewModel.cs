namespace JavidElectronics.Areas.Admin.ViewModels.Blog
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string From { get; set; } = null!;
        public List<ViewModels.Tag.ListItemViewModel>? Tags { get; set; }
        public List<int>? TagIds { get; set; }
        public List<ViewModels.Category.ListItemViewModel>? Categories { get; set; }
        public List<int>? CategoryIds { get; set; }
    }
}
