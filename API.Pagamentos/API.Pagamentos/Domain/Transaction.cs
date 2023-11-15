namespace API.Pagamentos.Domain
{
    public class Transaction
    {
        public long Id { get; set; }
        public double ValueTransaction { get; set; }
        public User Sender { get; set; }
        public int SenderId { get; set; }
        public User Receiver { get; set; }
        public int ReceiverId { get; set; }
        public DateTime LocalDateTime { get; set; }
    }
}
