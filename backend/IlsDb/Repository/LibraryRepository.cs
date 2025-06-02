using Microsoft.EntityFrameworkCore;

namespace IlsDb.Repository
{
    public class LibraryRepository
    {
        private readonly LibraryDbContext _dbContext;

        public LibraryRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> IsEmpty()
        {
            return !await this._dbContext.Libraries.AnyAsync(library => true);
        }
    }
}
