using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class Category : BaseEntity<int>, IAuditable
    {
        public string? Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        public List<ProductCategory>? ProductCategories { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
