using Microsoft.AspNetCore.Mvc;
using IlsDb.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IlsDb.Object;

namespace IlsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubsidiaryController : Controller
    {
        private readonly SubsidiaryService _subsidiaryService;
        private readonly UserTypeService _userTypeService;

        public SubsidiaryController(SubsidiaryService subsidiaryService, UserTypeService userTypeService)
        {
            this._subsidiaryService = subsidiaryService;
            this._userTypeService = userTypeService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IResult> Create(
            [FromBody] SubsidiaryRecieve subsidiary
        ) {
            Claim? jwtTokenClaim = User.FindFirst("UserType");

            if (jwtTokenClaim == null)
                return Results.Unauthorized();

            string userType = jwtTokenClaim.Value;

            if (!(userType == this._userTypeService.ADMIN || userType == this._userTypeService.LIBRARIAN))
                return Results.Unauthorized();

            await this._subsidiaryService.Create(subsidiary);

            return Results.Ok();
        }
    }
}
