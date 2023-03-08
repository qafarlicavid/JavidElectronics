using JavidElectronics.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JavidElectronics.Database.Configurations
{
    public class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder
                .ToTable("ProductCategories");

            builder
                .HasKey(bc => new { bc.CategoryId, bc.ProductId });

            builder
               .HasOne(bc => bc.Product)
               .WithMany(b => b.ProductCategories)
               .HasForeignKey(bc => bc.ProductId);

            builder
                .HasOne(bc => bc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }
    }
}
