using IlsDb.Entity.RelationEntity;
using Microsoft.EntityFrameworkCore;

namespace IlsDb.Repository
{
    public class UserBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserBookRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Add(UserBookEntity userBook)
        {
            await this._dbContext.UserBookEntities.AddAsync(userBook);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsRecord(Guid userId, Guid bookId)
        {
            return await this._dbContext.UserBookEntities.AnyAsync(userBook => userBook.UserId == userId && userBook.BookId == bookId);
        }
    }
}
