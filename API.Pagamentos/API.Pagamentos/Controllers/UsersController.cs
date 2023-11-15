using API.Pagamentos.Domain;
using API.Pagamentos.DTOs;
using API.Pagamentos.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Pagamentos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

            if (users == null)
            {
                return NotFound("No users found");
            }

            var userDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDTO);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(long? id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with Id {id} not found");
            }
            else if (user.Id != id)
            {
                return BadRequest("Bad request");
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUserAsync([FromBody] UserDTO userDTO)
        {
            if (await _userRepository.GetEmailUserAsync(userDTO.Email))
                return BadRequest("Error: E-mail already exists.");
            else if (await _userRepository.GetCPF_CNPJUserAsync(userDTO.CPF_CNPJ))
                return BadRequest("Error: CPF/CNPJ already exists");

            var user = _mapper.Map<User>(userDTO);
            await _userRepository.CreateAsync(user);

            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}
