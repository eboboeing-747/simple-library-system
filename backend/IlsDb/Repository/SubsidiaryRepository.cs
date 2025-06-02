using Microsoft.EntityFrameworkCore;

namespace IlsDb.Repository
{
    public class SubsidiaryRepository
    {
        public readonly LibraryDbContext _dbContext;

        public SubsidiaryRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> IsEmpty()
        {
            return !await this._dbContext.Subsidiaries.AnyAsync(library => true);
        }
    }
}
