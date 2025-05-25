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

        public bool Exists(string login)
        {
            return this._dbContext.Users.Any(user => user.Login == login);
        }

        public async Task<UserEntity?> GetByLogin(string login)
        {
            return await this._dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Login == login);
        }

        public async Task<bool> Create(UserEntity user)
        {
            if (this.Exists(user.Login))
                return false;

            user.Id = Guid.NewGuid();
            // TODO: check if user.UserType exists

            await this._dbContext.AddAsync(user);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public bool IsEmpty()
        {
            return this._dbContext.Users.Any();
        }
    }
}
