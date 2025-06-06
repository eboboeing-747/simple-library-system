namespace IlsDb.Object
{
    public class BookRecieve
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public long PublishDate { get; set; }
        public string ISBN { get; set; } = string.Empty;
    }
}
