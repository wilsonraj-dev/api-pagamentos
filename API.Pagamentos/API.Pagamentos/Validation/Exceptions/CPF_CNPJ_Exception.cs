namespace API.Pagamentos.Validation.Exceptions
{
    public class CPF_CNPJ_Exception : Exception
    {
        public CPF_CNPJ_Exception() : base("CPF or CNPJ already exists!") { }
    }
}
