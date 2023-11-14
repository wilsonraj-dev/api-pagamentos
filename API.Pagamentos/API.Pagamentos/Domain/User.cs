namespace API.Pagamentos.Domain
{
    public  class User
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public UserType UserType { get; set; }
    }
}
