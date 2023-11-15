using API.Pagamentos.Context;
using API.Pagamentos.Domain;
using API.Pagamentos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Pagamentos.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int? id)
        {
            return await _context.Transactions.FindAsync(id) ?? throw new ArgumentNullException();
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
