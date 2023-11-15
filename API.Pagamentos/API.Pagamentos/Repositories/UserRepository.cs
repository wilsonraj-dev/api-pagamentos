using API.Pagamentos.Context;
using API.Pagamentos.Domain;
using API.Pagamentos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Pagamentos.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int? id)
        {
            return await _context.Users.FindAsync(id) ?? throw new ArgumentNullException();
        }

        public async Task<bool> GetCPF_CNPJUserAsync(string CPF_CNPJ)
        {
            return await _context.Users.AnyAsync(x => x.CPF_CNPJ == CPF_CNPJ);
        }

        public async Task<bool> GetEmailUserAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
