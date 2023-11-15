using API.Pagamentos.Domain;

namespace API.Pagamentos.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Transaction> GetByIdAsync(int? id);
        Task<Transaction> CreateAsync(Transaction transaction);
    }
}
