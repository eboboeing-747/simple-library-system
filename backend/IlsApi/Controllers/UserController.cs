using IlsDb.Entity.BaseEntity;
using IlsDb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IlsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("register")]
        async public Task<IResult> Register(
            [FromBody] UserObject user
        ) {
            UserEntity userEntity = new UserEntity
            {
                Id = (Guid)user.Id,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex ?? false,
                UserType = (Guid)user.UserType
            };

            await this._userService.Register(userEntity);

            return Results.Created();
        }

        [HttpPost("login")]
        async public Task<IResult> Login(
            [FromBody] UserObject user
        ) {
            string? token = await this._userService.Login(user.Login, user.Password);

            if (token == null)
                return Results.Unauthorized();

            HttpContext.Response.Cookies.Append("jwtToken", token);
            return Results.Ok();
        }

        [Authorize]
        [HttpGet("test")]
        async public Task<IResult> Test()
        {
            return Results.Ok();
        }
    }

    public record UserObject(
        Guid? Id,
        string? Login,
        string? Password,
        string? FirstName,
        string? LastName,
        bool? Sex,
        Guid? UserType
    );
}
