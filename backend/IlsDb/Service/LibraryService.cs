using IlsDb.Entity.BaseEntity;
using IlsDb.Object;
using IlsDb.Repository;
using Microsoft.AspNetCore.Http;

namespace IlsDb.Service
{
    public class LibraryService
    {
        private readonly LibraryRepository _libraryRepository;
        private readonly SubsidiaryRepository _subsidiaryRepository;

        public LibraryService(LibraryRepository libraryRepository, SubsidiaryRepository subsidiaryRepository)
        {
            this._libraryRepository = libraryRepository;
            this._subsidiaryRepository = subsidiaryRepository;
        }

        public async Task<IResult> Get()
        {
            LibraryEntity? library = await this._libraryRepository.Get();

            if (library == null)
                return Results.NotFound();

            return Results.Ok(library);
        }

        public async Task CreateFirstLibraryIfNotExists()
        {
            if (await this._libraryRepository.Get() != null)
                return;

            LibraryEntity library = new LibraryEntity
            {
                Id = Guid.NewGuid(),
                Name = "your library name",
                Description = "this is a library description. It is visible to anyone visiting your library",
                LogoPath = "https://img.favpng.com/22/3/15/computer-icons-public-library-icon-design-png-favpng-f6uRzdiZz2pY5w7F5nMML571M.jpg"
            };

            await this._libraryRepository.Create(library);
        }

        public async Task Update(LibraryObject library)
        {
            await this._libraryRepository.Update(library);
        }
    }
}
