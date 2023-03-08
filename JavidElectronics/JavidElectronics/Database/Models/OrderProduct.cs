using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class OrderProduct : BaseEntity<int>, IAuditable
    {
        public string? OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
