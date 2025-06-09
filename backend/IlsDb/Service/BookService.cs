using IlsDb.Entity.BaseEntity;
using IlsDb.Object;
using IlsDb.Repository;
using Microsoft.AspNetCore.Http;

namespace IlsDb.Service
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public async Task<IResult> Get(Guid Id)
        {
            BookEntity? book = await this._bookRepository.GetById(Id);

            if (book == null)
                return Results.NotFound();

            return Results.Ok(book);
        }

        public async Task<List<BookEntity>> Find(string query)
        {
            List<BookEntity> books = await this._bookRepository.Find(query);

            return books;
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
