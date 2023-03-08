using JavidElectronics.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JavidElectronics.Database.Configurations
{
    public class BlogTagConfigurations : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder
               .ToTable("BlogTags");
        }
    }
}
