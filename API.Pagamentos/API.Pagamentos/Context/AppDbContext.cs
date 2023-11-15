using API.Pagamentos.Context.Configuration;
using API.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Pagamentos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op) { }

        public DbSet<User> Users { get; set; }
        public DbSet<CommonUser> CommonUsers { get; set; }
        public DbSet<Shopkeeper> Shopkeepers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CommomUserConfiguration());
            modelBuilder.ApplyConfiguration(new ShopkeeperConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
