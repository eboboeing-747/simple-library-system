using IlsDb.Entity.BaseEntity;
using IlsDb.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IlsDb.Utility;
using System.Security.Claims;

namespace IlsDb.Service
{
    public class UserService
    {
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
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(this._jwtOptions.ValidityDurationMinutes)
            );

            string JwtTokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return JwtTokenString;
        }

        private static  string Hash(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hashedPassword;
        }

        private static bool Verify(string password, string passwordHashed)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHashed);
        }

        public async Task Register(UserEntity user)
        {
            UserEntity userToCreate = new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Password = UserService.Hash(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex,
                UserType = user.UserType
            };

            await this._userRepository.Create(userToCreate);
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
