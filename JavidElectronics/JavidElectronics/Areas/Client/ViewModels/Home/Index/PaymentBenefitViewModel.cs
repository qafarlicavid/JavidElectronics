namespace JavidElectronics.Areas.Client.ViewModels.Home.Index
{
    public class PaymentBenefitViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackGroundImageUrl { get; set; }
        public PaymentBenefitViewModel()
        {
        }

        public PaymentBenefitViewModel(int id, string title, string content, string backGroundImageUrl)
        {
            Id = id;
            Title = title;
            Content = content;
            BackGroundImageUrl = backGroundImageUrl;
        }
    }
}
