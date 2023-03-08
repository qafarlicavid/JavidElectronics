using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class BlogVideo : BaseEntity<int>, IAuditable
    {
        public string? VideoName { get; set; }
        public string? VideoNameInFileSystem { get; set; }

        public string? VideoURLFromBrowser { get; set; }

        public int? BlogId { get; set; }
        public Blog? Blog { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
