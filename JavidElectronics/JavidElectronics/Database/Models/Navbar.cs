using JavidElectronics.Database.Models.Common;

namespace JavidElectronics.Database.Models
{
    public class Navbar : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? ToURL { get; set; }
        public int Order { get; set; }
        public bool IsMain { get; set; }
        public bool IsOnHeader { get; set; }
        public bool IsOnFooter { get; set; }

        public List<SubNavbar>? subNavbars { get; set; }
    }
}
