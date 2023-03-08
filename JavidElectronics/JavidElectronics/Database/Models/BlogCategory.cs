using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class BlogCategory : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
