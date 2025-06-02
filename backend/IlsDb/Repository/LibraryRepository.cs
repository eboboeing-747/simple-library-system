using IlsDb.Entity.BaseEntity;
using IlsDb.Object;
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

        public async Task Create(LibraryEntity library)
        {
            await this._dbContext.Libraries.AddAsync(library);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<LibraryEntity?> Get()
        {
            return await this._dbContext.Libraries
                .AsNoTracking()
                .FirstOrDefaultAsync(library => true);
        }

        public async Task<bool> IsEmpty()
        {
            return !await this._dbContext.Libraries.AnyAsync(library => true);
        }

        public async Task Update(LibraryObject libraryToUpdate)
        {
            await this._dbContext.Libraries
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(library => library.Name, libraryToUpdate.Name)
                    .SetProperty(library => library.Description, libraryToUpdate.Description)
                    .SetProperty(library => library.LogoPath, libraryToUpdate.LogoPath)
                );
        }
    }
}
