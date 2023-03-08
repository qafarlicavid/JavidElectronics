using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class Contact : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
