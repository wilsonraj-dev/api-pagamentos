using API.Pagamentos.Domain;
using API.Pagamentos.DTOs;
using API.Pagamentos.Repositories;
using API.Pagamentos.Repositories.Interfaces;
using API.Pagamentos.Services.Interfaces;
using API.Pagamentos.Validation.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Pagamentos.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;

        public UserService(IUserRepository user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _user.GetUsersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(long? id)
        {
            var user = await _user.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> GetCPF_CNPJUserDTOAsync(string CPF_CNPJ)
        {
            var existsCPF_CNPJ = !await _user.GetCPF_CNPJUserAsync(CPF_CNPJ);
            if (!existsCPF_CNPJ)
            {
                throw new CPF_CNPJ_Exception();
            }

            return existsCPF_CNPJ;
        }

        public async Task<bool> GetEmailUserDTOAsync(string email)
        {
            var existsEmail = !await _user.GetEmailUserAsync(email);
            if (!existsEmail)
            {
                throw new EmailException();
            }

            return existsEmail;
        }

        public async Task CreateAsync(UserDTO userDTO)
        {
            var userEntity = _mapper.Map<User>(userDTO);
            await _user.CreateAsync(userEntity);
        }

        public async Task UpdateAsync(UserDTO userDTO)
        {
            var userEntity = _mapper.Map<User>(userDTO);
            await _user.UpdateAsync(userEntity);
        }
    }
}
