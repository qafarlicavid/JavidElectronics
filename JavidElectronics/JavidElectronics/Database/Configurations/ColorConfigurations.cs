using JavidElectronics.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JavidElectronics.Database.Configurations
{
    public class ColorConfigurations : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder
               .ToTable("Colors");
        }
    }
}
