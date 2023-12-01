using API.Pagamentos.Domain;
using API.Pagamentos.DTOs;
using API.Pagamentos.Repositories.Interfaces;
using API.Pagamentos.Services.Interfaces;
using AutoMapper;

namespace API.Pagamentos.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transaction;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transaction, IMapper mapper)
        {
            _transaction = transaction;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsAsync()
        {
            var transactions = await _transaction.GetTransactionsAsync();
            return _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public async Task<TransactionDTO> GetByIdAsync(int? id)
        {
            var transaction = await _transaction.GetByIdAsync(id);
            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task CreateAsync(TransactionDTO transactionDTO)
        {
            var transactionEntity = _mapper.Map<Transaction>(transactionDTO);
            await _transaction.CreateAsync(transactionEntity);
        }
    }
}
