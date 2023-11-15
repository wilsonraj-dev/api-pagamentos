using API.Pagamentos.Validation;

namespace API.Pagamentos.Domain
{
    public class CommonUser : User
    {
        public string CPF { get; set; } = string.Empty;

        public CommonUser(string CPF) => ValidateDomain(CPF);

        private void ValidateDomain(string CPF)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(CPF), "Invalid CPF. CPF is required for common users.");
            DomainExceptionValidation.When(CPF.Length < 11, "Invalid CPF. CPF must have 11 characters.");

            this.CPF = CPF;
        }
    }
}
