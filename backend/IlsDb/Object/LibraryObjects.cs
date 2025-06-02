namespace IlsDb.Object
{
    public record LibraryObject
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string LogoPath { get; init; } = string.Empty;
    }
}
