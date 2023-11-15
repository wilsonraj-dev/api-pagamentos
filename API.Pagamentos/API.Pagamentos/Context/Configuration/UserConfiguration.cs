using API.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Pagamentos.Context.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(55).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Balance).HasPrecision(10, 3);
        }
    }
}
