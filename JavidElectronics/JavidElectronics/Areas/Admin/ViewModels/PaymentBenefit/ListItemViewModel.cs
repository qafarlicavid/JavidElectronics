namespace JavidElectronics.Areas.Admin.ViewModels.PaymentBenefit
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string content, string imageUrl)
        {
            Id = id;
            Title = title;
            Content = content;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
