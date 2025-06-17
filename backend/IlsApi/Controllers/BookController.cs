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
        private readonly UserBookService _userBookService;

        public BookController(
            BookService bookService,
            UserTypeService userTypeService,
            UserBookService userBookService
        ) {
            this._bookService = bookService;
            this._userTypeService = userTypeService;
            this._userBookService = userBookService;
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

        [HttpGet("get/{Id}")]
        public async Task<IResult> Get(Guid Id)
        {
            return await this._bookService.Get(Id);
        }

        [HttpGet("find")]
        public async Task<IResult> Find(string query)
        {
            List<BookEntity> books = await this._bookService.Find(query);

            if (books.Count < 1)
                return Results.NotFound();

            return Results.Ok(books);
        }

        [Authorize]
        [HttpGet("isTaken/{bookId}")]
        public async Task<IResult> IsTaken(Guid bookId)
        {
            Claim? jwtTokenClaim = User.FindFirst("Id");

            if (jwtTokenClaim == null)
                return Results.Unauthorized();

            if (!Guid.TryParse(jwtTokenClaim.Value, out Guid userId))
                return Results.BadRequest();

            return await this._userBookService.IsTaken(userId, bookId);
        }

        [Authorize]
        [HttpPost("take/{bookId}")]
        public async Task<IResult> Take(Guid bookId)
        {
            Claim? jwtTokenClaim = User.FindFirst("Id");

            if (jwtTokenClaim == null)
                return Results.Unauthorized();

            if (!Guid.TryParse(jwtTokenClaim.Value, out Guid userId))
                return Results.BadRequest();

            return await this._userBookService.Add(userId, bookId);
        }

        [Authorize]
        [HttpDelete("return/{bookId}")]
        public async Task<IResult> Return(Guid bookId)
        {
            Claim? jwtTokenClaim = User.FindFirst("Id");

            if (jwtTokenClaim == null)
                return Results.Unauthorized();

            if (!Guid.TryParse(jwtTokenClaim.Value, out Guid userId))
                return Results.BadRequest();

            return await this._userBookService.Return(userId, bookId);
        }

        [HttpGet("taken/{userId}")]
        public async Task<IResult> Taken(Guid userId)
        {
            return await this._userBookService.GetBooksByUserId(userId);
        }
    }
}