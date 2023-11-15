using API.Pagamentos.Validation;

namespace API.Pagamentos.Domain
{
    public class Shopkeeper : User
    {
        public string CNPJ = string.Empty;

        public Shopkeeper(string CNPJ) => ValidateDomain(CNPJ);

        private void ValidateDomain(string CNPJ)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(CNPJ), "Invalid CNPJ. CNPJ is required for shopkeepers.");
            DomainExceptionValidation.When(CNPJ.Length < 14, "Invalid CNPJ. CNPJ must have 14 characters.");

            this.CNPJ = CNPJ;
        }
    }
}
