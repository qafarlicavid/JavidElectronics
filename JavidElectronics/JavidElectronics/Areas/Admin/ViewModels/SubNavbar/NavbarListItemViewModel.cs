namespace JavidElectronics.Areas.Admin.ViewModels.SubNavbar
{
    public class NavbarListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public NavbarListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
