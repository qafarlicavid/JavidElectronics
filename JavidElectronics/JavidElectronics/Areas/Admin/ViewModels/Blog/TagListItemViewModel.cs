namespace JavidElectronics.Areas.Admin.ViewModels.Blog
{
    public class TagListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public TagListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
