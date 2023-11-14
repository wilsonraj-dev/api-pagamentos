namespace API.Pagamentos.Domain
{
    public class Usuario
    {
        public string Nome { get; set; } = string.Empty;
        public string SobreNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
