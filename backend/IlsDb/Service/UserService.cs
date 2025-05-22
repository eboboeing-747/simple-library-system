using IlsDb.Entity.BaseEntity;
using IlsDb.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IlsDb.Utility;
using System.Security.Claims;
using IlsDb.Object;
using Microsoft.AspNetCore.Http;

namespace IlsDb.Service
{
    public class UserService
    {
        private readonly int USER_FIELD_LENTH = 4;

        private UserRepository _userRepository;
        private JwtOptions _jwtOptions;

        public UserService(UserRepository userRepository, JwtOptions jwtOptions)
        {
            this._userRepository = userRepository;
            this._jwtOptions = jwtOptions;
        }

        private string GenerateJwtToken(UserEntity user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserType", user.UserType.ToString()) // TODO: pull UserType from the database
            };

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

        public bool ValidateField(string? field, ref string errorMessage, string fieldName = "")
        {
            if (field == null)
            {
                errorMessage = $"field {fieldName} can not be null";
                return false;
            }
            if (field.Length < this.USER_FIELD_LENTH)
            {
                errorMessage = $"lenght of field {fieldName} can not subseed {this.USER_FIELD_LENTH}";
                return false;
            }

            return true;
        }

        public async Task<IResult> Register(UserObject user)
        {
            string errorMessage = string.Empty;

            if (!ValidateField(user.Login, ref errorMessage, "login"))
                return Results.BadRequest(errorMessage);
            if (!ValidateField(user.Password, ref errorMessage, "password"))
                return Results.BadRequest(errorMessage);
            if (!ValidateField(user.FirstName, ref errorMessage, "first name"))
                return Results.BadRequest(errorMessage);
            if (!ValidateField(user.LastName, ref errorMessage, "last name"))
                return Results.BadRequest(errorMessage);
            if (user.UserType == null)
                return Results.BadRequest($"no such UserType Id {user.UserType}");

            Console.WriteLine("[Reginster] Ok");

            UserEntity userToCreate = new UserEntity
            {
                Id = Guid.NewGuid(),
                Login = user.Login,
                Password = UserService.Hash(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex ?? false,
                UserType = (Guid)user.UserType
            };

            bool isCreated = await this._userRepository.Create(userToCreate);
            if (isCreated)
                return Results.Created();
            else
                return Results.BadRequest("user with this login already exists");
        }

        public async Task<string?> Login(string login, string password)
        {
            UserEntity? user = await this._userRepository.GetByLogin(login);

            if (user == null)
                return null;

            bool isValid = UserService.Verify(password, user.Password);

            if (!isValid)
                return null;

            string jwtToken = this.GenerateJwtToken(user);
            return jwtToken;
        }
    }
}
