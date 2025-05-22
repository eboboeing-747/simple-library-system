using IlsDb.Entity.BaseEntity;
using IlsDb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IlsDb.Object;

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
            return await this._userService.Register(user);

            // return Results.Created();
        }

        [HttpPost("login")]
        async public Task<IResult> Login(
            [FromBody] UserObject user
        ) {
            if (user.Login == null)
                return Results.BadRequest("user.login can not be null");
            if (user.Password == null)
                return Results.BadRequest("user.password can not be null");

            string? token = await this._userService.Login(user.Login, user.Password);

            Console.WriteLine(user);

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
}
