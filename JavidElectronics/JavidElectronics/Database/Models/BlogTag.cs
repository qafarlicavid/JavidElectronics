using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class BlogTag : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
