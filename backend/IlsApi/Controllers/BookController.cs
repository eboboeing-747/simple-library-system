using IlsDb.Service;
using Microsoft.AspNetCore.Mvc;
using IlsDb.Object;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IlsDb.Entity.BaseEntity;

namespace IlsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly UserTypeService _userTypeService;

        public BookController(BookService bookService, UserTypeService userTypeService)
        {
            this._bookService = bookService;
            this._userTypeService = userTypeService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IResult> Create(
            [FromBody] BookRecieve book
        ) {
            Claim? jwtTokenClaim = User.FindFirst("UserType");

            if (jwtTokenClaim == null)
                return Results.Unauthorized();

            string userType = jwtTokenClaim.Value;

            if (!(userType == this._userTypeService.ADMIN || userType == this._userTypeService.LIBRARIAN))
                return Results.Unauthorized();

            await this._bookService.Create(book);

            return Results.Ok();
        }

        [HttpGet("find")]
        public async Task<IResult> Find(string query)
        {
            List<BookEntity> books = await this._bookService.Find(query);

            if (books.Count < 1)
                return Results.NotFound();

            return Results.Ok(books);
        }
    }
}
