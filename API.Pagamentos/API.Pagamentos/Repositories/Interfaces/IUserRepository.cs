using API.Pagamentos.Domain;

namespace API.Pagamentos.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetByIdAsync(int? id);
        Task<User> CreateAsync(User user);
    }
}
