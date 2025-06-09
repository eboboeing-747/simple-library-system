using IlsDb.Repository;
using IlsDb.Entity.RelationEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using IlsDb.Entity.BaseEntity;

namespace IlsDb.Service
{
    public class UserBookService
    {
        private readonly UserBookRepository _userBookRepository;
        private readonly BookRepository _bookRepository;

        public UserBookService(
            UserBookRepository userBookRepository,
            BookRepository bookRepository
        ) {
            this._userBookRepository = userBookRepository;
            this._bookRepository = bookRepository;
        }

        public async Task<IResult> Add(Guid userId, Guid bookId)
        {
            if (await this._bookRepository.GetById(bookId) == null)
                return Results.BadRequest();

            if (await this._userBookRepository.ExistsRecord(userId, bookId))
                return Results.Conflict();

            UserBookEntity userBook = new UserBookEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BookId = bookId
            };

            await this._userBookRepository.Add(userBook);
            return Results.Created();
        }

        public async Task<IResult> GetBooksByUserId(Guid userId)
        {
            List<Guid> bookIds = await this._userBookRepository.GetBooksByUserId(userId);
            List<BookEntity> books = new();

            foreach (Guid bookId in bookIds)
            {
                BookEntity? book = await this._bookRepository.GetById(bookId);

                if (book == null)
                    continue;

                books.Add(book);
            }

            return Results.Ok(books);
        }
    }
}
