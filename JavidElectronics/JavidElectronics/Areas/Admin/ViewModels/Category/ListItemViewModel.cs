namespace JavidElectronics.Areas.Admin.ViewModels.Category
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ListItemViewModel()
        {

        }
    }
}
