using IlsDb.Entity.BaseEntity;
using IlsDb.Object;
using IlsDb.Repository;

namespace IlsDb.Service
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public async Task Create(BookRecieve book)
        {
            BookEntity bookToCreate = new BookEntity
            {
                Id = Guid.NewGuid(),
                Name = book.Name,
                Description = book.Description,
                ImagePath = book.ImagePath,
                PublishDate = DateTimeOffset.FromUnixTimeMilliseconds(book.PublishDate).UtcDateTime,
                ISBN = book.ISBN
            };

            await this._bookRepository.Create(bookToCreate);
        }
    }
}
