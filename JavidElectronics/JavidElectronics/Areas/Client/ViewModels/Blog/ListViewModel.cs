namespace JavidElectronics.Areas.Client.ViewModels.Blog
{
    public class ListViewModel
    {
        public List<JavidElectronics.Areas.Admin.ViewModels.Tag.ListItemViewModel>? Tags { get; set; }

        public string SearchQuery { get; set; }
        public int? TagId { get; set; }
        public ListViewModel()
        {

        }

        public ListViewModel(List<JavidElectronics.Areas.Admin.ViewModels.Tag.ListItemViewModel>? tags)
        {
            Tags = tags;
        }

        public List<ListItemViewModel>? Blogs { get; set; }
    }
}
