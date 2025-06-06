﻿namespace IlsDb.Object
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
    }

    public record UserUpdate
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string pfpPath { get; init; } = string.Empty;
    }

    public record UserReturn : UserUpdate
    {
        public required Guid Id { get; init; }
        public string Login { get; init; } = string.Empty;
        public bool Sex { get; init; }
        public string UserType { get; init; } = string.Empty;
    }
}
