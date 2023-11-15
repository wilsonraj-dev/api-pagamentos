using API.Pagamentos.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Pagamentos.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(55)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lastname is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText(true)]
        public string Password { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Balance")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "User type is required")]
        public UserType UserType { get; set; }

        public ICollection<Transaction> TransactionSender { get; set; }
        public ICollection<Transaction> TransactionReceiver { get; set; }
    }
}
