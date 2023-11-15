using API.Pagamentos.Domain;
using API.Pagamentos.DTOs;
using AutoMapper;

namespace API.Pagamentos.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
        }
    }
}
