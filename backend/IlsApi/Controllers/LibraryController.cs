using IlsDb.Service;
using Microsoft.AspNetCore.Mvc;

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
    }
}
