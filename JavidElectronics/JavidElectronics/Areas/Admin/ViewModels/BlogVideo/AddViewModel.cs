using JavidElectronics.Database.Models.Enums;

namespace JavidElectronics.Areas.Admin.ViewModels.BlogVideo
{
    public class AddViewModel
    {
        public OrderStatus? Order { get; set; }
        public IFormFile? Video { get; set; }
        public string? VideoURLFromBrowser { get; set; }
    }
}
