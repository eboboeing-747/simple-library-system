using IlsDb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IlsDb.Object;

namespace IlsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibraryController : Controller
    {
        private readonly LibraryService _libraryService;

        public LibraryController(LibraryService libraryService)
        {
            this._libraryService = libraryService;
        }

        [HttpGet("get")]
        public async Task<IResult> Get()
        {
            return await this._libraryService.Get();
        }

        // [Authorize]
        [HttpPost("update")]
        public async Task<IResult> Update(
            [FromBody] LibraryObject library
        ) {
            /*
            Claim? jwtTokenUserType = User.FindFirst("Type");

            if (jwtTokenUserType == null)
                return Results.Unauthorized();
            */

            await this._libraryService.Update(library);

            return Results.Ok();
        }
    }
}
