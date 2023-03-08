using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JavidElectronics.Database.Models;

namespace JavidElectronics.Database.Configurations
{
    public class ProductColorConfigurations : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder
                .ToTable("ProductColors");

            builder
                .HasKey(bc => new { bc.ProductId, bc.ColorId });

            builder
                .HasOne(bc => bc.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(bc => bc.ProductId);

            builder
                .HasOne(bc => bc.Color)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(bc => bc.ColorId);
        }
    }
}