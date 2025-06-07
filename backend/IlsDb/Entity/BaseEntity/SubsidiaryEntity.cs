namespace IlsDb.Entity.BaseEntity
{
    public class SubsidiaryEntity
    {
        public Guid Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public Guid ReadingRoomId { get; set; }
    }
}
