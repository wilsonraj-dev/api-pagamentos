namespace API.Pagamentos.Domain
{
    public class Transacao
    {
        public double ValorTransacao { get; set; }
        public User? Remetente { get; set; }
        public User? Receptor { get; set; }
        public DateTime HoraTransferencia { get; set; }
    }
}
