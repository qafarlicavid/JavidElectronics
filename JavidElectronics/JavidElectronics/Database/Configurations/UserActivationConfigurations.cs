using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JavidElectronics.Database.Models;

namespace JavidElectronics.Database.Configurations
{
    public class UserActivationConfigurations : IEntityTypeConfiguration<UserActivation>
    {
        public void Configure(EntityTypeBuilder<UserActivation> builder)
        {
            builder
               .ToTable("UserActivations");
        }
    }
}