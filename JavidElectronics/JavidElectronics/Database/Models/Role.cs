using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class Role : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        
        public List<User>? Users { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
