namespace API.Pagamentos.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string error) : base(error) { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }

        public static void ValidateCPF(string CPF, string error)
        {
            if (string.IsNullOrWhiteSpace(CPF))
                throw new DomainExceptionValidation(error);

            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "").Replace("-", "");

            if (CPF.Length != 11)
                throw new DomainExceptionValidation(error);

            if (!long.TryParse(CPF, out long parsedCpf))
                throw new DomainExceptionValidation(error);

            bool sameDigits = CPF.Distinct().Count() == 1;
            if (sameDigits)
                throw new DomainExceptionValidation(error);

            int[] firstDigitsMultipliers = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondDigitsMultipliers = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cpfWhitoutDigits = CPF.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpfWhitoutDigits[i].ToString()) * firstDigitsMultipliers[i];
            }

            int rest = sum % 11;
            int firstVerifierDigit = rest < 2 ? 0 : 11 - rest;

            cpfWhitoutDigits += firstVerifierDigit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpfWhitoutDigits[i].ToString()) * secondDigitsMultipliers[i];
            }

            rest = sum % 11;
            int secondVerifierDigit = rest < 2 ? 0 : 11 - rest;

            if (!CPF.EndsWith(firstVerifierDigit.ToString() + secondVerifierDigit.ToString()))
                throw new DomainExceptionValidation(error);
        }

        public static void ValidateCNPJ(string CNPJ, string error)
        {
            if (string.IsNullOrWhiteSpace(CNPJ))
                throw new DomainExceptionValidation(error);

            CNPJ = CNPJ.Trim();
            CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            if (CNPJ.Length != 14)
                throw new DomainExceptionValidation(error);

            if (!long.TryParse(CNPJ, out long parsedCnpj))
                throw new DomainExceptionValidation(error);

            int[] firstDigitMultipliers = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondDigitMultipliers = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cnpjWithoutDigits = CNPJ.Substring(0, 12);

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(cnpjWithoutDigits[i].ToString()) * firstDigitMultipliers[i];
            }

            int remainder = sum % 11;
            int firstVerifierDigit = remainder < 2 ? 0 : 11 - remainder;

            cnpjWithoutDigits += firstVerifierDigit;
            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(cnpjWithoutDigits[i].ToString()) * secondDigitMultipliers[i];
            }

            remainder = sum % 11;
            int secondVerifierDigit = remainder < 2 ? 0 : 11 - remainder;

            if (!CNPJ.EndsWith(firstVerifierDigit.ToString() + secondVerifierDigit.ToString()))
                throw new DomainExceptionValidation(error);
        }
    }
}
