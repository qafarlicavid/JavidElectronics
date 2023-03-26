namespace JavidElectronics.Areas.Admin.ViewModels.Color
{
    public class AddViewModel
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public AddViewModel()
        {

        }
        public AddViewModel(string name, DateTime createdDate, DateTime updatedDate)
        {
            Name = name;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
    }
}