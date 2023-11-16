using API.Pagamentos.Domain;
using API.Pagamentos.DTOs;
using API.Pagamentos.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Net;
using System.Xml.Linq;

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
            var userReceiver = _userRepository.GetByIdAsync(transactionDTO.ReceiverId).Result;
            var userSender = _userRepository.GetByIdAsync(transactionDTO.SenderId).Result;

            if (userReceiver == null)
                return NotFound("Receiver not found");
            else if (userSender == null)
                return NotFound("Sender not found");

            ValidTransction(userReceiver, userSender, Convert.ToDecimal(transactionDTO.ValueTransaction));
            if (await ConsultExternalService("https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc") == false)
                return BadRequest("Not autorized");

            userSender.Balance -= Convert.ToDecimal(transactionDTO.ValueTransaction);
            userReceiver.Balance += Convert.ToDecimal(transactionDTO.ValueTransaction);

            await _userRepository.UpdateAsync(userReceiver);
            await _userRepository.UpdateAsync(userSender);

            var transaction = _mapper.Map<Transaction>(transactionDTO);
            await _transactionRepository.CreateAsync(transaction);

            if (await ConsultExternalService("https://run.mocky.io/v3/54dc2cf1-3add-45b5-b5a9-6bf7e7f1f4a6"))
                Console.WriteLine($"You received a transfer from {userReceiver.Name} worth {transaction.ValueTransaction.ToString("F2", CultureInfo.InvariantCulture)}");

            return Ok(_mapper.Map<TransactionDTO>(transaction));
        }

        private void ValidTransction(User userReceiver, User userSender, decimal valueTransaction)
        {
            if (userSender.UserType == UserType.Shopkeeper)
                throw new Exception($"User {userSender.Name} its a Shopkeeper. He cannot make transfers");
            else if (userSender.Balance < valueTransaction)
                throw new Exception($"User {userSender.Name} has insufficient balance to make this transfer");
            else if (userSender.Id == userReceiver.Id)
                throw new Exception($"Receiver and Sender are the same Users");
        }

        private async Task<bool> ConsultExternalService(string url)
        {

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
    }
}
