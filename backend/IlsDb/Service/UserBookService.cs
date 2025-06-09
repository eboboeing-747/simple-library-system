using IlsDb.Repository;
using IlsDb.Entity.RelationEntity;
using Microsoft.AspNetCore.Http;

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
    }
}
