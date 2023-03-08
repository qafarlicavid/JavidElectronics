using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class UserActivation : BaseEntity<int>, IAuditable
    {
        public string? ActivationUrl { get; set; }
        public string? ActivationToken { get; set; }
        public DateTime ExpireDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
