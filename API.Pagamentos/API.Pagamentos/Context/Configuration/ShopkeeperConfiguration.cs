using API.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Pagamentos.Context.Configuration
{
    public class ShopkeeperConfiguration : IEntityTypeConfiguration<Shopkeeper>
    {
        public void Configure(EntityTypeBuilder<Shopkeeper> builder)
        {
            builder.Property(x => x.CNPJ).HasMaxLength(18).IsRequired();
        }
    }
}
