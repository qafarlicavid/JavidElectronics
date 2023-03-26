using JavidElectronics.Database.Models.Enums;

namespace JavidElectronics.Areas.Admin.ViewModels.BlogImage
{
    public class UpdateViewModel
    {
        public string? ImageUrL { get; set; }
        public OrderStatus? Order { get; set; }
        public IFormFile? Image { get; set; }
    }
}
