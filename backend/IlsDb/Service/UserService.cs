using IlsDb.Entity.BaseEntity;
using IlsDb.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IlsDb.Utility;
using System.Security.Claims;
using IlsDb.Object.User;
using Microsoft.AspNetCore.Http;

namespace IlsDb.Service
{
    public class UserService
    {
        private readonly int USER_FIELD_LENTH = 4;

        private readonly UserRepository _userRepository;
        private readonly JwtOptions _jwtOptions;
        private readonly UserTypeService _userTypeService;

        public UserService(
            UserRepository userRepository,
            JwtOptions jwtOptions,
            UserTypeService userTypeService
        ) {
            this._userRepository = userRepository;
            this._jwtOptions = jwtOptions;
            this._userTypeService = userTypeService;
        }

        private string GenerateJwtToken(UserEntity user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserType", this._userTypeService.Resolve(user.UserType)) // TODO: pull UserType from the database
            };

            Console.WriteLine($"[GenerateJwtToken]{user.UserType}: {this._userTypeService.Resolve(user.UserType)} ({user.UserType})");

            SigningCredentials signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(this._jwtOptions.SecretKey)
                ),
                SecurityAlgorithms.HmacSha256
            );

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(this._jwtOptions.ValidityDurationMinutes)
            );

            string JwtTokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return JwtTokenString;
        }

        private static string Hash(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hashedPassword;
        }

        private static bool Verify(string password, string passwordHashed)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHashed);
        }

        public async Task<IResult> Register(UserRegister user)
        {
            UserEntity userToCreate = new UserEntity
            {
                Id = Guid.NewGuid(),
                Login = user.Login,
                Password = UserService.Hash(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex,
                UserType = this._userTypeService.USER_ID
            };

            bool isCreated = await this._userRepository.Create(userToCreate);

            if (isCreated)
                return Results.Created();

            return Results.BadRequest("user with this login already exists");
        }

        public async Task CreateFirstAdmin(UserCredentials adminCredentials)
        {
            UserEntity admin = new UserEntity
            {
                Id = Guid.NewGuid(),
                Login = adminCredentials.Login,
                Password = UserService.Hash(adminCredentials.Password),
                FirstName = "first",
                LastName = "admin",
                Sex = false,
                UserType = this._userTypeService.ADMIN_ID
            };

            bool isCreated = await this._userRepository.Create(admin);

            return;
        }

        public async Task<(string?, UserReturn?)> Login(string login, string password)
        {
            UserEntity? user = await this._userRepository.GetByLogin(login);

            if (user == null)
                return (null, null);

            bool isValid = UserService.Verify(password, user.Password);

            if (!isValid)
                return (null, null);

            Console.WriteLine($"[Login]{user.UserType}");

            string jwtToken = this.GenerateJwtToken(user);
            UserReturn userToReturn = new UserReturn
            {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                pfpPath = user.pfpPath,
                Sex = user.Sex,
                UserType = this._userTypeService.Resolve(user.UserType)
            };

            return (jwtToken, userToReturn);
        }

        public async Task<IResult> Authorize(Guid UserId)
        {
            UserEntity? userEntity = await this._userRepository.GetById(UserId);

            if (userEntity == null)
            {
                return Results.Unauthorized();
            }

            UserReturn user = new UserReturn
            {
                Id = userEntity.Id,
                Login = userEntity.Login,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Sex = userEntity.Sex,
                UserType = this._userTypeService.Resolve(userEntity.UserType),
                pfpPath = userEntity.pfpPath
            };

            Console.WriteLine(user);

            return Results.Ok(user);
        }

        public async Task<IResult> Update(Guid userId, UserUpdate user)
        {
            bool isSuccess = await this._userRepository.Update(userId, user);

            return isSuccess ? Results.Ok() : Results.BadRequest();
        }

        public bool IsEmpty()
        {
            return this._userRepository.IsEmpty();
        }
    }
}
