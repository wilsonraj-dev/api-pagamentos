using API.Pagamentos.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace API.Pagamentos.DTOs
{
    public class TransactionDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Transaction's value is required")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Value")]
        public double ValueTransaction { get; set; }
        public User Sender { get; set; }
        public long SenderId { get; set; }
        public User Receiver { get; set; }
        public long ReceiverId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LocalDateTime { get; set; }
    }
}
