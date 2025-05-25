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
        private Guid _ADMIN_ID;
        private Guid _LIBRARIAN_ID;
        private Guid _USER_ID;

        public string ADMIN { get; init; } = "admin";
        public string USER { get; init; } = "user";
        public string LIBRARIAN { get; init; } = "librarian";
        public Guid ADMIN_ID { get { return this._ADMIN_ID; } }
        public Guid LIBRARIAN_ID { get { return this._LIBRARIAN_ID; } }
        public Guid USER_ID { get { return this._USER_ID; } }

        public UserTypeService(UserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
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
            this._ADMIN_ID = await this._userTypeRepository.GetIdByTypeName(this.ADMIN);
            this._LIBRARIAN_ID = await this._userTypeRepository.GetIdByTypeName(this.LIBRARIAN);
            this._USER_ID = await this._userTypeRepository.GetIdByTypeName(this.USER);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{this.ADMIN_ID}: {this.ADMIN}");
            Console.WriteLine($"{this.LIBRARIAN_ID}: {this.LIBRARIAN}");
            Console.WriteLine($"{this.USER_ID}: {this.USER}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
