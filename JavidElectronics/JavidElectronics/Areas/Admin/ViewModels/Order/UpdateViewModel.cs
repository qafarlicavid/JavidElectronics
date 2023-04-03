using JavidElectronics.Database.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace JavidElectronics.Areas.Admin.ViewModels.Order
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
