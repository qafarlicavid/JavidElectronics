namespace JavidElectronics.Areas.Client.ViewModels.Benefit
{
    public class BenefitListItemViewModel
    {
        public BenefitListItemViewModel(string name, string content, string imageUrl)
        {
            Name = name;
            Content = content;
            ImageUrl = imageUrl;
        }

        public string Name { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
