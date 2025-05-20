namespace IlsDb.Entity.RelationEntity
{
    public class AuthorBookEntity
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }
    }
}
