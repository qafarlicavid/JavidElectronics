using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class Color : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductColor>? ProductColors { get; set; }
    }
}
