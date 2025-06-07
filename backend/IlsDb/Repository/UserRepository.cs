using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using IlsDb.Object;

namespace IlsDb.Repository
{
    public class UserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserRepository(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        async public Task<bool> Exists(string login)
        {
            return await this._dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == login);
        }

        async public Task<bool> Exists(Guid Id)
        {
            return await this._dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Id == Id);
        }

        public async Task<UserEntity?> GetByLogin(string login)
        {
            return await this._dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == login);
        }

        public async Task<UserEntity?> GetById(Guid Id)
        {
            return await this._dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == Id);
        }

        public async Task<bool> Create(UserEntity user)
        {
            if (await this.Exists(user.Login))
                return false;

            user.Id = Guid.NewGuid();

            await this._dbContext.Users.AddAsync(user);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Guid userId, UserUpdate userToUpdate)
        {
            if (!await this.Exists(userId))
                return false;

            await this._dbContext.Users
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(user => user.FirstName, userToUpdate.FirstName)
                    .SetProperty(user => user.LastName, userToUpdate.LastName)
                    .SetProperty(user => user.PfpPath, userToUpdate.pfpPath)
                );

            return true;
        }

        public bool IsEmpty()
        {
            return !this._dbContext.Users.Any(user => true);
        }
    }
}
