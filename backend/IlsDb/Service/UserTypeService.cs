using IlsDb.Entity.BaseEntity;
using IlsDb.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Drawing;

namespace IlsDb.Service
{
    public class UserTypeService
    {
        private readonly UserTypeRepository _userTypeRepository;
        private static Guid _ADMIN_ID;
        private static Guid _LIBRARIAN_ID;
        private static Guid _USER_ID;

        public string ADMIN { get; init; } = "admin";
        public string USER { get; init; } = "user";
        public string LIBRARIAN { get; init; } = "librarian";
        public Guid ADMIN_ID { get { return UserTypeService._ADMIN_ID; } }
        public Guid LIBRARIAN_ID { get { return UserTypeService._LIBRARIAN_ID; } }
        public Guid USER_ID { get { return UserTypeService._USER_ID; } }

        public UserTypeService(UserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        public string Resolve(Guid Id)
        {
            if (Id == UserTypeService._ADMIN_ID)
                return ADMIN;
            else if (Id == UserTypeService._LIBRARIAN_ID)
                return LIBRARIAN;
            else if (Id == UserTypeService._USER_ID)
                return USER;

            return "???";
        }

        public async Task CreateBaseTypes()
        {
            UserTypeEntity admin = new UserTypeEntity {
                Id = Guid.NewGuid(),
                Name = this.ADMIN
            };

            UserTypeEntity librarian = new UserTypeEntity {
                Id = Guid.NewGuid(),
                Name = this.LIBRARIAN
            };

            UserTypeEntity user = new UserTypeEntity {
                Id = Guid.NewGuid(),
                Name = this.USER
            };

            await this._userTypeRepository.Create(admin);
            await this._userTypeRepository.Create(librarian);
            await this._userTypeRepository.Create(user);
        }

        public async Task BindUserTypeIds()
        {
            UserTypeService._ADMIN_ID = await this._userTypeRepository.GetIdByTypeName(this.ADMIN);
            UserTypeService._LIBRARIAN_ID = await this._userTypeRepository.GetIdByTypeName(this.LIBRARIAN);
            UserTypeService._USER_ID = await this._userTypeRepository.GetIdByTypeName(this.USER);
        }
    }
}
