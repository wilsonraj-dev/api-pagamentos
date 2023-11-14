namespace API.Pagamentos.Domain
{
    public class Lojista : Usuario
    {
        public string CNPJ { get; set; } = string.Empty;
    }
}
