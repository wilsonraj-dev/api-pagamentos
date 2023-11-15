using API.Pagamentos.Domain;
using API.Pagamentos.DTOs;
using API.Pagamentos.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Pagamentos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionRepository transactionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetTransactionsAsync();

            if (transactions == null)
            {
                return NotFound("No transactions found");
            }

            var transactionDTO = _mapper.Map<IEnumerable<TransactionDTO>>(transactions);
            return Ok(transactionDTO);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDTO>> GetUserByIdAsync(int? id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
            {
                return NotFound($"Transaction with Id {id} not found");
            }
            else if (transaction.Id != id)
            {
                return BadRequest("Bad request");
            }

            var transactionDTO = _mapper.Map<TransactionDTO>(transaction);
            return Ok(transactionDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransactionDTO>> CreateTransactionAsync([FromBody] TransactionDTO transactionDTO)
        {
            var user = _userRepository.GetByIdAsync(transactionDTO.ReceiverId).Result;
            if (user == null)
                return NotFound("Receiver not found");

            ValidTransction(user, Convert.ToDecimal(transactionDTO.ValueTransaction));
            if (await AuthorizeTransaction() == false)
                return BadRequest("Not autorized");


            return null;
        }

        private void ValidTransction(User user, decimal valueTransaction)
        {
            if (user.UserType == UserType.Shopkeeper)
                throw new Exception($"User {user.Name} its a Shopkeeper. He cannot make transfers");
            else if (user.Balance < valueTransaction)
                throw new Exception($"User {user.Name} has insufficient balance to make this transfer");
        }

        private async Task<bool> AuthorizeTransaction()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc");
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
    }
}
