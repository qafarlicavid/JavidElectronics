using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class SubNavbar : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? ToURL { get; set; }
        public int Order { get; set; }

        public int NavbarId { get; set; }
        public Navbar? Navbar { get; set; }

    }
}
