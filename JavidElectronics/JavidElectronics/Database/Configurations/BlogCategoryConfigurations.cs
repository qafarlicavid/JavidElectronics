using JavidElectronics.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JavidElectronics.Database.Configurations
{
    public class BlogCategoryConfigurations : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder
                .ToTable("BlogCategories");

            builder
                .HasOne(bi => bi.Blog)
                .WithMany(b => b.BlogCategories)
                .HasForeignKey(bi => bi.BlogId);
        }
    }
}
