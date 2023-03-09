using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class ProductTag : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
