using API.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Pagamentos.Context.Configuration
{
    public class CommomUserConfiguration : IEntityTypeConfiguration<CommonUser>
    {
        public void Configure(EntityTypeBuilder<CommonUser> builder)
        {
            builder.Property(x => x.CPF).HasMaxLength(18).IsRequired();
        }
    }
}
