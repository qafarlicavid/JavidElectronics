using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class Creator : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public string? ImageName { get; set; } //<original_name>.<extension>
        public string? ImageNameInFileSystem { get; set; } //Guid.<extension>

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
