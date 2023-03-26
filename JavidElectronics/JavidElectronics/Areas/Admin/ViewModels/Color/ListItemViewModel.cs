namespace JavidElectronics.Areas.Admin.ViewModels.Color
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}