using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class Tag : BaseEntity<int>, IAuditable
    {
        public string? Title { get; set; }

        public List<ProductTag>? ProductTags { get; set; }
        public List<BlogTag>? BlogTags { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
