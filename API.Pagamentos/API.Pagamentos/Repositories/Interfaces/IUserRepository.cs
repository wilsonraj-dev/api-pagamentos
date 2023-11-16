using API.Pagamentos.Domain;

namespace API.Pagamentos.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetByIdAsync(long? id);
        Task<bool> GetCPF_CNPJUserAsync(string CPF_CNPJ);
        Task<bool> GetEmailUserAsync(string email);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
    }
}
