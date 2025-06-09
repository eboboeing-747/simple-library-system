using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace IlsDb.Repository
{
    public class BookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<BookEntity>> Find(string query)
        {
            return await this._dbContext.Books
                .AsNoTracking()
                .Where(book => EF.Functions.ILike(book.Name, $@"%{query}%"))
                .ToListAsync();
        }

        public async Task Create(BookEntity book)
        {
            await this._dbContext.Books.AddAsync(book);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<BookEntity?> GetById(Guid bookId)
        {
            return await this._dbContext.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(book => book.Id == bookId);
        }
    }
}
