namespace JavidElectronics.Areas.Admin.ViewModels.Tag
{
    public class ListItemViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public ListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}