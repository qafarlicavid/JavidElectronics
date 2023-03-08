using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JavidElectronics.Database.Models;

namespace JavidElectronics.Database.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .ToTable("Users");

            builder
               .HasOne(u => u.Basket)
               .WithOne(b => b.User)
               .HasForeignKey<Basket>(u => u.UserId);
        }
    }
}
