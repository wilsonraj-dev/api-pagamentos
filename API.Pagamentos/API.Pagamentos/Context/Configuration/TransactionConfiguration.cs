using API.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Pagamentos.Context.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ValueTransaction).HasPrecision(10, 3).IsRequired();

            builder.HasOne(x => x.Sender)
                   .WithMany(x => x.Transactions)
                   .HasForeignKey(x => x.SenderId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Receiver)
                   .WithMany(x => x.Transactions)
                   .HasForeignKey(x => x.ReceiverId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
