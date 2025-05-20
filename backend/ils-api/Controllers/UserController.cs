using ils_database.Entity.BaseEntity;
using ils_database.Service;
using Microsoft.AspNetCore.Mvc;

namespace ils_api.Controllers
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
        async public Task<ActionResult> Register(
            [FromBody] UserObject user
        ) {
            UserEntity userEntity = new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex,
                UserType = user.UserType
            };

            await this._userService.Register(userEntity);

            return Created();
        }
    }

    public record UserObject(Guid Id, string Login, string Password, string FirstName, string LastName, bool Sex, Guid UserType);
}
