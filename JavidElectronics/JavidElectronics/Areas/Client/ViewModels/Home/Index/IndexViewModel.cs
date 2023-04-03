namespace JavidElectronics.Areas.Client.ViewModels.Home.Index
{
    public class IndexViewModel
    {
        public List<SliderListItemViewModel>? Sliders { get; set; }
        public List<PaymentBenefitViewModel>? PaymentBenefits { get; set; }

        public List<ProductListItemViewModel>? Products { get; set; }
        public List<BlogListItemViewModel>? Blogs { get; set; }
    }
}
