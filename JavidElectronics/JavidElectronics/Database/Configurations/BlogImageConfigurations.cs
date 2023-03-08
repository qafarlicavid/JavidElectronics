using JavidElectronics.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JavidElectronics.Database.Configurations
{
    public class BlogImageConfigurations : IEntityTypeConfiguration<BlogImage>
    {
        public void Configure(EntityTypeBuilder<BlogImage> builder)
        {
            builder
                .ToTable("BlogImages");

            builder
                .HasOne(bi => bi.Blog)
                .WithMany(b => b.BlogImages)
                .HasForeignKey(bi => bi.BlogId);
        }
    }
}
