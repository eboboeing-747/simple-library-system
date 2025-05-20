using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace IlsDb.Repository
{
    public class UserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<UserEntity?> GetByLogin(string login)
        {
            return await this._dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == login);
        }

        public async Task Create(UserEntity user)
        {
            user.Id = Guid.NewGuid();

            await this._dbContext.AddAsync(user);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
