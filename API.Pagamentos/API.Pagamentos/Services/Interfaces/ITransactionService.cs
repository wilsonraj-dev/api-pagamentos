using API.Pagamentos.DTOs;

namespace API.Pagamentos.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetTransactionsAsync();
        Task<TransactionDTO> GetByIdAsync(int? id);
        Task CreateAsync(TransactionDTO transactionDTO);
    }
}
