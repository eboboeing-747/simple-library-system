using IlsDb.Repository;

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

        public async Task<string> GetLibraryStatus()
        {
            if (await this._libraryRepository.IsEmpty())
                return "nolib";
            else if (await this._subsidiaryRepository.IsEmpty())
                return "nosub";

            return "ok";
        }
    }
}
