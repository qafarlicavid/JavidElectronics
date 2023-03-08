using JavidElectronics.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JavidElectronics.Database.Configurations
{
    public class BasketProductConfigurations : IEntityTypeConfiguration<BasketProduct>
    {
        public void Configure(EntityTypeBuilder<BasketProduct> builder)
        {
            builder
                .ToTable("BasketProducts");

            builder
              .HasOne(b => b.Basket)
              .WithMany(basket => basket.BasketProducts)
              .HasForeignKey(bi => bi.BasketId);

            builder
              .HasOne(p => p.Product)
              .WithMany(product => product.BasketProducts)
              .HasForeignKey(pi => pi.ProductId);

        }
    }
}
