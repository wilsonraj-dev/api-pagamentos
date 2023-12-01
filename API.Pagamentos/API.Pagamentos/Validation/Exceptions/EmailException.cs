namespace API.Pagamentos.Validation.Exceptions
{
    public class EmailException : Exception
    {
        public EmailException() : base("Email already exists") { }
    }
}
