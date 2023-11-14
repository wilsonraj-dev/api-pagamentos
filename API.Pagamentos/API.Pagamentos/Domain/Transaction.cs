namespace API.Pagamentos.Domain
{
    public class Transaction
    {
        public long Id { get; set; }
        public double ValueTransaction { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
        public DateTime LocalDateTime { get; set; }
    }
}
