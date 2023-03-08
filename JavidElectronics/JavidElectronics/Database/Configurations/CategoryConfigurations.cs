using JavidElectronics.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JavidElectronics.Database.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
               .ToTable("Categories");

            builder
               .HasOne(c => c.Parent)
               .WithMany(pc => pc.Categories)
               .HasForeignKey(c => c.ParentId);
        }
    }
}
