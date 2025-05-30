using IlsDb.Entity.BaseEntity;
using IlsDb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IlsDb.Object.User;
using System.Security.Claims;

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
            [FromBody] UserRegister user
        ) {
            return await this._userService.Register(user);

            // return Results.Created();
        }

        [HttpPost("login")]
        async public Task<IResult> Login(
            [FromBody] UserCredentials user
        ) {
            (string? token, UserReturn? userToReturn) = await this._userService.Login(user.Login, user.Password);

            if (token == null)
                return Results.Unauthorized();

            HttpContext.Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });
            return Results.Ok(userToReturn);
        }

        [Authorize]
        [HttpGet("authorize")]
        async public Task<IResult> Authorize()
        {
            Claim? jwtTokenClaim = User.FindFirst("Id");

            if (jwtTokenClaim == null)
                return Results.Unauthorized();

            Guid.TryParse(jwtTokenClaim.Value, out Guid userId);

            return await this._userService.Authorize(userId);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IResult> Update(
            [FromBody] UserUpdate user
        ) {
            Claim? jwtTokenClaim = User.FindFirst("Id");

            if (jwtTokenClaim == null)
                return Results.Unauthorized();

            Guid.TryParse(jwtTokenClaim.Value, out Guid userId);

            return await this._userService.Update(userId, user);
        }

        [HttpGet("logout")]
        async public Task<IResult> Logout()
        {
            string token = "logout";

            HttpContext.Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });
            return Results.Ok();
        }

        [HttpGet("test")]
        async public Task<IResult> Test()
        {
            return Results.Ok();
        }
    }
}
