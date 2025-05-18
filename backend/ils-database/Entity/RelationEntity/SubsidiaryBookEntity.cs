namespace ils_database.Entity.RelationEntity
{
    public class SubsidiaryBookEntity
    {
        public Guid Id { get; set; }
        public Guid SubsidiaryId { get; set; }
        public Guid BookId { get; set; }
        public int Amount { get; set; }
    }
}
