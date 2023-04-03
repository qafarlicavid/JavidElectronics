using JavidElectronics.Database.Models.Enums;

namespace JavidElectronics.Areas.Admin.ViewModels.Order
{
    public class ListItemViewModel
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public ListItemViewModel(string id, DateTime createdtAt, OrderStatus status, decimal total)
        {
            Id = id;
            CreatedAt = createdtAt;
            Status = status;
            Total = total;
        }

    }
}
