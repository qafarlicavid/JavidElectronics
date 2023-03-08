using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JavidElectronics.Database.Models;

namespace JavidElectronics.Database.Configurations
{
    public class ProductImageConfigurations : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder
                .ToTable("ProductImages");

            builder
                .HasOne(p => p.Products)
                .WithMany(i => i.ProductImages)
                .HasForeignKey(pi => pi.ProductId);
        }
    }
}
