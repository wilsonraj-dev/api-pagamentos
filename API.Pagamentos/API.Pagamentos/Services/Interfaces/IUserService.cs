using API.Pagamentos.DTOs;

namespace API.Pagamentos.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetByIdAsync(long? id);
        Task<bool> GetCPF_CNPJUserDTOAsync(string CPF_CNPJ);
        Task<bool> GetEmailUserDTOAsync(string email);
        Task CreateAsync(UserDTO user);
        Task UpdateAsync(UserDTO user);
    }
}
