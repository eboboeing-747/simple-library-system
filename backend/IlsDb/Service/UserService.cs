﻿using IlsDb.Entity.BaseEntity;
using IlsDb.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IlsDb.Utility;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using IlsDb.Object;
using System.Runtime.CompilerServices;

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

            return Results.Conflict($"{{\"error\": \"user with this login already exists\"}}");
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

        public UserReturn Convert(UserEntity userEntity)
        {
            UserReturn user = new UserReturn
            {
                Id = userEntity.Id,
                Login = userEntity.Login,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Sex = userEntity.Sex,
                UserType = this._userTypeService.Resolve(userEntity.UserType),
                pfpPath = userEntity.PfpPath
            };

            return user;
        }

        public async Task<(string?, UserReturn?)> Login(string login, string password)
        {
            UserEntity? user = await this._userRepository.GetByLogin(login);

            if (user == null)
                return (null, null);

            bool isValid = UserService.Verify(password, user.Password);

            if (!isValid)
                return (null, null);

            string jwtToken = this.GenerateJwtToken(user);
            UserReturn userToReturn = this.Convert(user);

            return (jwtToken, userToReturn);
        }

        public async Task<IResult> Authorize(Guid UserId)
        {
            UserEntity? userEntity = await this._userRepository.GetById(UserId);

            if (userEntity == null)
            {
                return Results.Unauthorized();
            }

            UserReturn user = this.Convert(userEntity);

            return Results.Ok(user);
        }

        public async Task<IResult> Update(Guid userId, UserUpdate user)
        {
            bool isSuccess = await this._userRepository.Update(userId, user);

            return isSuccess ? Results.Ok() : Results.BadRequest();
        }

        public async Task<IResult> Get(Guid Id)
        {
            UserEntity? userEntity = await this._userRepository.GetById(Id);

            if (userEntity == null)
                return Results.NotFound();

            UserReturn user = this.Convert(userEntity);

            return Results.Ok(user);
        }

        public async Task<List<UserReturn>> Find(string query)
        {
            List<UserEntity> userEntities = await this._userRepository.Find(query);
            List<UserReturn> users = new();

            foreach (UserEntity userEntity in userEntities)
            {
                users.Add(this.Convert(userEntity));
            }

            return users;
        }

        public bool IsEmpty()
        {
            return this._userRepository.IsEmpty();
        }
    }
}
