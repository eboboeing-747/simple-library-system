namespace IlsDb.Entity.BaseEntity
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; } = string.Empty;
    }
}
