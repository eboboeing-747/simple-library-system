namespace IlsDb.Entity.BaseEntity
{
    public class LibraryEntity
    {
        public Guid Id { get; set; }
        public Guid AdministratorId { get; set; }
        public Guid SubsidiaryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
