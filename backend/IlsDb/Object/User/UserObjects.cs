namespace IlsDb.Object.User
{
    public record UserCredentials
    {
        public string Login { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }

    public record UserRegister : UserCredentials
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public bool Sex { get; init; }
        public Guid UserType { get; init; }
    }
}
