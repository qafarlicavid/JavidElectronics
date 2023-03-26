namespace JavidElectronics.Areas.Admin.ViewModels.Tag
{
    public class AddViewModel
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public AddViewModel()
        {

        }
        public AddViewModel(string title, DateTime createdDate, DateTime updatedDate)
        {
            Title = title;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }


    }
}