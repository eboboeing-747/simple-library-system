namespace IlsDb.Object
{
    public record UserObject(
        Guid? Id,
        string? Login,
        string? Password,
        string? FirstName,
        string? LastName,
        bool? Sex,
        Guid? UserType
    );
}