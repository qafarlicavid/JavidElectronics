using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JavidElectronics.Database.Models;

namespace JavidElectronics.Database.Configurations
{
    public class SubNavbarConfigurations : IEntityTypeConfiguration<SubNavbar>
    {
        public void Configure(EntityTypeBuilder<SubNavbar> builder)
        {
            builder
               .HasOne(b => b.Navbar)
               .WithMany(a => a.subNavbars)
               .HasForeignKey(b => b.NavbarId);
        }
    }
}
