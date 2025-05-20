namespace IlsDb.Entity.RelationEntity
{
    public class UserBookEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
