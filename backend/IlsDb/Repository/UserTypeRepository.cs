using IlsDb.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace IlsDb.Repository
{
    public class UserTypeRepository
    {
        private readonly LibraryDbContext _dbContext;
        
        public UserTypeRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(UserTypeEntity userType)
        {
            if (await this.Exists(userType.Name))
                return false;

            await this._dbContext.UserTypes.AddAsync(userType);
            await this._dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(Guid Id)
        {
            return await this._dbContext.UserTypes
                .AsNoTracking()
                .AnyAsync(userType => userType.Id == Id);
        }

        public async Task<bool> Exists(string userTypeName)
        {
            return await this._dbContext.UserTypes
                .AsNoTracking()
                .AnyAsync(userType => userType.Name == userTypeName);
        }

        /*
        public async Task<Guid> GetIdByTypeName(string name)
        {
            UserTypeEntity? userType = await this._dbContext.UserTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(type => type.Name == name);

            if (userType == null)
            {
                Console.WriteLine("user type should exist at startup");
                System.Environment.Exit(1);
            }

            return userType.Id;
        }
        */

        public async Task<Guid> GetIdByTypeName(string name)
        {
            UserTypeEntity? userType = await this._dbContext.UserTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(type => type.Name == name);

            if (userType == null)
            {
                Console.WriteLine("userType should exist at startup");
                System.Environment.Exit(1);
            }

            return userType.Id;
        }
    }
}
