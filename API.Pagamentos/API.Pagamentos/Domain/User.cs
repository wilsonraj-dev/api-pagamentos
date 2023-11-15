using API.Pagamentos.Validation;

namespace API.Pagamentos.Domain
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string CPF_CNPJ { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public UserType UserType { get; set; }
        public ICollection<Transaction>? TransactionSender { get; set; }
        public ICollection<Transaction>? TransactionReceiver { get; set; }

        public User() { }

        public User(string name, string lastName, string email, string password, string CPF_CNPJ, decimal balance, UserType userType)
        {
            ValidateDomain(name, lastName, email, password, CPF_CNPJ, balance, userType);
        }

        public User(int id, string name, string lastName, string email, string password, string CPF_CNPJ, decimal balance, UserType userType)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            Id = id;

            ValidateDomain(name, lastName, email, password, CPF_CNPJ, balance, userType);
        }

        private void ValidateDomain(string name, string lastName, string email, string password, string CPF_CNPJ, decimal balance, UserType userType)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required.");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(lastName), "Invalid last name. Last name is required.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Invalid email. Email is required.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(password), "Invalid password. Password is required.");
            DomainExceptionValidation.When(password.Length < 5, "Invalid password, too short, minimum 5 characters.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(CPF_CNPJ), "Invalid CPF/CNPJ. Field is required.");
            DomainExceptionValidation.When(CPF_CNPJ.Length > 14, "Invalid CPF/CNPJ, too long, maximun 14 characters.");
            DomainExceptionValidation.When(balance < 0, "The balance cannot be less than 0.");
            DomainExceptionValidation.When(Convert.ToInt32(userType) != 0 && Convert.ToInt32(userType) != 1, "Invalid user type, sets a valid user type (0 or 1).");

            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            this.CPF_CNPJ = CPF_CNPJ;
            Balance = balance;
            UserType = userType;
        }
    }
}
