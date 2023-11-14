namespace API.Pagamentos.Domain
{
    public class Transacao
    {
        public double ValorTransacao { get; set; }
        public Usuario? Remetente { get; set; }
        public Usuario? Receptor { get; set; }
        public DateTime HoraTransferencia { get; set; }
    }
}
