using JavidElectronics.Database.Models.Enums;

namespace JavidElectronics.Areas.Admin.ViewModels.BlogImage
{
    public class AddViewModel
    {
        public OrderStatus? Order { get; set; }
        public IFormFile? Image { get; set; }
    }
}
